using Microsoft.Extensions.Options;
using iTekstovi.API.Models;
using iTekstovi.API.AppClasses;

namespace iTekstovi.API.Controllers
{
    public class ArtistsController : DALController<ArtistModel>
    {
        public ArtistsController(IOptions<ApiConfig> configValues) : 
            base (configValues)
        {

        }
    }
}