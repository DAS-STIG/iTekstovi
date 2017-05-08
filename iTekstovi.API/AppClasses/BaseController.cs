using Microsoft.AspNetCore.Mvc;
using iTekstovi.API.DAL;

namespace iTekstovi.API.AppClasses
{
    public class BaseController : Controller
    {
        internal IDataService _DataService { get; set; }
        public BaseController(IDataService _dataService) 
        {
            _DataService = _dataService;
        }
    }
}
