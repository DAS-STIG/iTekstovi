using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using iTekstovi.API.AppClasses;
using iTekstovi.API.DAL;

namespace iTekstovi.API.Controllers
{
    public class DALController<T> : Controller where T : class
    {
        private IRepository<T> _Repository { get; set; }
        

        public DALController(IOptions<ApiConfig> configValues)
        {
            _Repository = new Repository<T>(configValues);
        }
        
        [HttpGet]
        public virtual async Task<List<T>> List(Guid? id) 
        {
            return await this._Repository.All(id.ToString());
        }

        [HttpGet]
        public virtual async Task<T> Get(int id)
        {
            T modelObj = await this._Repository.Get(id.ToString());

            if (modelObj == null) 
            {
                Response.StatusCode = 404; // not found
            }

            return modelObj;
        }

        [HttpPost]
        public virtual async Task Post([FromBody]T modelObj)
        {
            if (!ModelState.IsValid)
            {   
                Response.StatusCode = 400; // bad request 
                return;
            }

            bool saveStatus = await this._Repository.Save(modelObj);    
            if (!saveStatus) 
            {
                Response.StatusCode = 500; // internal server error
            }
        }

        [HttpPut]
        public virtual async Task Put(int id, [FromBody]T modelObj)
        {
            if (!ModelState.IsValid)
            {   
                Response.StatusCode = 400; // bad request 
                return;
            }

            // make sure object exists before trying to update it
            T searchResult = await this._Repository.Get(id.ToString());
            if (searchResult != null)
            {
                //modelObj.Id = id;

                bool saveStatus = await _Repository.Save(modelObj);    
                if (!saveStatus) 
                {
                    Response.StatusCode = 500; // internal server error
                }         
            }
            else 
            {
                Response.StatusCode = 404; // not found
            }
        }

        [HttpGet]
        public virtual async Task Delete(int id)
        {   
            T modelObj = await _Repository.Get(id.ToString());

            if (modelObj == null) 
            {
                Response.StatusCode = 404; // not found
                return;
            }

            bool saveStatus = await _Repository.Delete(id.ToString());
            if (!saveStatus) 
            {
                Response.StatusCode = 500; // internal server error
            }
        }
    }
}
