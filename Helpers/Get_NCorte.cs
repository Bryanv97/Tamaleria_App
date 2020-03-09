using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Tamaleria_Miguelena.Model;
using Tamaleria_Miguelena.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;

namespace Tamaleria_Miguelena.Helpers
{
    public class Get_NCorte : PropertyChange
    {
        Repository_Api n_corte = new Repository_Api();
        Get_ID Get = new Get_ID();
        private ObservableCollection<CorteModel> _corte;
        public ObservableCollection<CorteModel> corte
        {
            get { return _corte; }
            set { _corte = value; OnPropertyChanged(); }
        }

        public async Task<int> get_ncorte() 
        {
            int n = 0;
            var ret = await n_corte.numero_corte(new CorteModel { sucursal = Get.get_sucursal()});
            corte = new ObservableCollection<CorteModel>(ret);
            foreach(CorteModel rip in corte) 
            {
                n=n+1;
            }
            return n;

        }

    }
}
