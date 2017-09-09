using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGCM.Models
{
    public class DataHora
    {
        protected DateTime? _dataHora { get; set; }

        public DataHora()
        {
            _dataHora = null;
        }

        public DataHora(string dataHora)
        {
            this.Converter(dataHora);
        }

        public DataHora(DateTime dataHora)
        {
            this.Valor = dataHora;
        }

        public DateTime Valor
        {
            get
            {
                if (_dataHora == null)
                    return new DateTime();

                return _dataHora.Value;
            }

            set
            {
                _dataHora = value;
            }
        }

        public bool Converter(string dataHoraString)
        {
            try
            {
                //2017-07-16T10:30:00
                int year = int.Parse(dataHoraString.Substring(0, 4));
                int month = int.Parse(dataHoraString.Substring(5, 2));
                int day = int.Parse(dataHoraString.Substring(8, 2));

                int hour = int.Parse(dataHoraString.Substring(11, 2));
                int minute = int.Parse(dataHoraString.Substring(14, 2));
                int second = int.Parse(dataHoraString.Substring(17, 2));

                _dataHora = new DateTime(year, month, day, hour, minute, second);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public override string ToString()
        {
            if (_dataHora == null)
                return string.Empty;

            //2017-07-16T10:30:00
            return string.Format("{0}-{1}-{2}T{3}:{4}:{5}", _dataHora.Value.Year,
                                                            _dataHora.Value.Month.ToString("D2"),
                                                            _dataHora.Value.Day.ToString("D2"),
                                                            _dataHora.Value.Hour.ToString("D2"),
                                                            _dataHora.Value.Minute.ToString("D2"),
                                                            _dataHora.Value.Second.ToString("D2"));
        }
    }
}