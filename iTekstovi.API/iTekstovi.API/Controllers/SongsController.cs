using Microsoft.Extensions.Options;
using iTekstovi.API.Models;
using iTekstovi.API.AppClasses;

namespace iTekstovi.API.Controllers
{
    public class SongsController : DALController<SongModel>
    {
        public SongsController(IOptions<ApiConfig> configValues) : 
            base (configValues)
        {

        }
    }
}