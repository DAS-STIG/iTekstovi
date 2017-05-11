using System;
using System.Collections.Generic;
using iTekstovi.API.DAL.PgSql;
using iTekstovi.API.Models;

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

       public DataService() 
       {
           this.Songs = new Repository<SongModel>(); 
           this.Artists = new Repository<ArtistModel>(); 
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