using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using Reponsitories;

namespace Services.Implementaions
{
    public class ReturnService : IReturnService
    {
        private readonly IReturnRepository repository;
        public ReturnService(IReturnRepository repository) => this.repository = repository;
        
        public List<Return> GetAllReturns() => repository.GetAll();
        
        public void ProcessReturn(Return returnItem) => repository.Add(returnItem);
    }
}
