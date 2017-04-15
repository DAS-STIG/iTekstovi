using iTekstovi.API.DAL.PgSql;
using iTekstovi.API.Models;

namespace iTekstovi.API.DAL
{
    public interface IDataService 
    {
        IRepository<SongModel> ModelName { get; set; }    
    }

    public class DataService : IDataService
    {
       public IRepository<SongModel> ModelName { get; set; }

       public DataService() 
       {
           this.ModelName = new Repository<SongModel>(); 
       }

       /// <summary>
       /// Overload to make unit testing easier. May also add PgSql to DI services 
       /// </summary>
       /// <param name="_pgSql"></param>
       public DataService(IPgSql _pgSql) 
       {
           this.ModelName = new Repository<SongModel>(_pgSql); 
       }
    }
}