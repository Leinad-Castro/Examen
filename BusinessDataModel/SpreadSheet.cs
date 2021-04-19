using ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDataModel
{
    public class SpreadSheet
    {
        public string NameSheet;
        private double ValorVna;
        private const double TASA = 0.12;
        List<mvInputs> model = new List<mvInputs>();

        public SpreadSheet(string name)
        {
            this.NameSheet = name;
        }
        public void set(List<mvInputs> model)
        {
            this.model = model;
        }
        public List<mvInputs> get()
        {
            return model;
        }

        public void calculate()
        {
            int i = 1;
            foreach (mvInputs item in model)
            {
                double k = Math.Pow((1 + TASA), i);
                ValorVna += item.Valor / k;
                i++;
            }
        }
        public double vna()
        {
            return this.ValorVna;
        }
    }
}
