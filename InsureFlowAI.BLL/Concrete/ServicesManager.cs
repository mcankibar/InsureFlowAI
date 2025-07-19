using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsureFlowAI.BLL.Services;
using InsureFlowAI.DAL.Abstract;
using InsureFlowAI.DAL.Models.DataModel;

namespace InsureFlowAI.BLL.Concrete
{
    public class ServicesManager : IServicesService
    {
        private readonly IServicesDal _servicesDal;
        public ServicesManager(IServicesDal servicesDal)
        {
            _servicesDal = servicesDal;
        }
        public void TInsert(Tbl_Services entity)
        {
            _servicesDal.Insert(entity);
        }
        public void TDelete(int id)
        {
            _servicesDal.Delete(id);
        }
        public void TUpdate(Tbl_Services entity)
        {
            _servicesDal.Update(entity);
        }
        public List<Tbl_Services> TGetAll()
        {
            return _servicesDal.GetAll().ToList();
        }
        public Tbl_Services TGetById(int id)
        {
            return _servicesDal.GetById(id);
        }
    }
}
