using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataCollector.Entities;
using System.Net.NetworkInformation;
using OmronFinsTCP.Net;

namespace DataCollector.Services.BLL
{
    interface IPLCDataService<T>
    {
        (bool IsError, string ErrorMessage, List<T> Datas) GetData();
    }
    public class OmronPLCDataService : IPLCDataService<Dose>
    {
        private short COUNTER_CELLS_FOR_READING = 499;
        private short START_CELL = 500;

        private static short COUNT_DATAROWS = 10;//количество ячеек для считывания в одном рецепте.
        private short PACKEGES = (short)(500 / COUNT_DATAROWS);//количество пакетов, которое можем считать за один раз (до 500 ячеек в одной передаче данных).

        private ConfigApp _configApp = null;

        public OmronPLCDataService(ConfigApp appSettings)
        {
            _configApp = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
        }


        public (bool IsError, string ErrorMessage, List<Dose> Datas) GetData()
        {
            if (!PingPLC())
            {
                return (true, "Ping is not found", null);
            }

            var result = GetPLCData();
            if(result.IsError)
            {
                return (true, result.ErrorMessage, null);
            }
            return (false, "", result.Datas);
   
        }


        private (bool IsError, string ErrorMessage, List<Dose> Datas) GetPLCData()
        {
            var datas = new List<Dose>();
            PlcMemory dm = PlcMemory.DM;
            short resultCountCells = 0;
            EtherNetPLC cp1 = new EtherNetPLC();
            try
            {
                cp1.Link(_configApp.IpAddress, _configApp.PortAddress, 300);

                cp1.ReadWord(dm, COUNTER_CELLS_FOR_READING, out resultCountCells);

                if (resultCountCells > 0)
                {
                    short total = (short)(resultCountCells / PACKEGES);//целое число пакетов (кратное числу возможному для приема пакетов за раз).
                    short mod = (short)(resultCountCells % PACKEGES);//остаток пакетов от целого числа
                    short[] data = new short[600];
                    for (int i = 0; i < total; i++)
                    {
                        cp1.ReadWords(dm, (short)(START_CELL + (i * PACKEGES * COUNT_DATAROWS)), (short)((PACKEGES * COUNT_DATAROWS)), out data);
                        datas.AddRange(TransformArrayToDose(data).ToArray());
                    }

                    if (mod > 0)
                    {
                        cp1.ReadWords(dm, (short)(START_CELL + ((short)total * PACKEGES * COUNT_DATAROWS)), (short)((mod * COUNT_DATAROWS)), out data);


                        datas.AddRange(TransformArrayToDose(data).ToArray());
                    }
                    cp1.WriteWord(dm, COUNTER_CELLS_FOR_READING, 0);
                }

               // cp1.WriteWord(dm, COUNTER_CELLS_FOR_READING, 0);
                return (false,"", datas.ToList());
            }
            catch
            {
                return (true, "Connection to PLC didn't establish", null);
            }
            finally
            {
                cp1.Close();
            }

        }

        private List<Dose> TransformArrayToDose(short[] data)
        {
            int c = data.Length;
            var listDoseDTO = new List<Dose>();
            for (int i = 0; i < data.Length; i += COUNT_DATAROWS)
            {
                var doseDto = new Dose();

                string dd = (data[2 + i] + "." + data[1 + i] + "." + data[0 + i]);
                var tt = Convert.ToDateTime(dd).ToShortDateString();
                doseDto.datedose = Convert.ToDateTime(tt);
                var time= Convert.ToDateTime(data[3 + i] + ":" + data[4 + i]);
                doseDto.timedose = time.TimeOfDay;
                doseDto.dosiernumber = data[5 + i];
                doseDto.componentnumber = data[6 + i];
                doseDto.setweight = data[7 + i];
                doseDto.realweight = data[8 + i];
                doseDto.errorweight = data[9 + i];

                listDoseDTO.Add(doseDto);
            }

            return listDoseDTO;
        }


        private bool PingPLC()
        {
            Ping ping = new Ping();
            PingReply rep;
            rep = ping.Send(_configApp.IpAddress, 1000);
            if (rep.Status == IPStatus.Success)//проверка статуса пинга
            {
                return  true;
            }
            else
            {
                return false;
            }
        }
    }
}