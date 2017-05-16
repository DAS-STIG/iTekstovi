using System;
using System.Collections.Generic;
using iTekstovi.API.DAL.PgSql;
using iTekstovi.API.Models;
using Microsoft.Extensions.Options;

using iTekstovi.API.AppClasses;

namespace iTekstovi.API.DAL
{
    public interface IDataService 
    {
        IRepository<SongModel> Songs { get; set; }    

        IRepository<ArtistModel> Artists { get; set; }
    }

    public class DataService : IDataService
    {
       public IRepository<SongModel> Songs { get; set; }

       public IRepository<ArtistModel> Artists { get; set; }

       public DataService(IOptions<ApiConfig> configValues) 
       {
           this.Songs = new Repository<SongModel>(configValues); 
           this.Artists = new Repository<ArtistModel>(configValues); 
       }

       /// <summary>
       /// Overload to make unit testing easier. May also add PgSql to DI services 
       /// </summary>
       /// <param name="_pgSql"></param>
       public DataService(IPgSql _pgSql) 
       {
           this.Songs = new Repository<SongModel>(_pgSql); 
           this.Artists = new Repository<ArtistModel>(_pgSql);
       }
    }
}