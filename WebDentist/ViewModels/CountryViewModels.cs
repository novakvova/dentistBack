using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDentist.ViewModels
{
    public class CountryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CountryAddViewModel
    {
        public string Name { get; set; }
        public string FlagImage { get; set; }
    }
}
