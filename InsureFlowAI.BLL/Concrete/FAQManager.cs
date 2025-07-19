using InsureFlowAI.DAL.Abstract;
using InsureFlowAI.DAL.EntityFramework;
using InsureFlowAI.DAL.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsureFlowAI.BLL.Services;

namespace InsureFlowAI.BLL.Concrete
{
    public class FAQManager : IFAQService
    {
        private readonly IFAQDal _faqDal;

        public FAQManager(IFAQDal fqDal)
        {
            _faqDal = fqDal;
        }

        public void TInsert(Tbl_FAQ entity)
        {
            _faqDal.Insert(entity);
        }

        public void TDelete(int id)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(Tbl_FAQ entity)
        {
            throw new NotImplementedException();
        }

        public List<Tbl_FAQ> TGetAll()
        {
            return _faqDal.GetAll().ToList();
        }

        public Tbl_FAQ TGetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
