using DominioCineProducto.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominioCineProducto.utils
{
    public class Calendario
    {
        public Calendario() { }
        public DateTime SumarDiasLaborales(DateTime? fechaInicial, int numDias)
        {          
            DateTime dt = DateTime.Parse(fechaInicial.ToString());

            for (int k = 1; k <= numDias; k++)
            {
                dt = dt.AddDays(1);
                while (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday || EsDiaFeriado(dt))
                {
                    dt = dt.AddDays(1);
                }
                
            }

            return dt;
        }

        public bool EsDiaFeriado(DateTime dia)
        {
            try
            {
                cineEntities db = new cineEntities();
                int diasFeriados = db.dia_feriado.Where(x => DbFunctions.TruncateTime(x.fecha) == DbFunctions.TruncateTime(dia)).Count();
                if (diasFeriados > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex) {
                return false;
            }
        }
    }
}
