using InsureFlowAI.DAL.Abstract;
using InsureFlowAI.DAL.Models.DataModel;
using InsureFlowAI.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsureFlowAI.DAL.EntityFramework
{
    public class EfFAQDal : GenericRepository<Tbl_FAQ>, IFAQDal
    {
        private readonly InsureFlowAIDBEntities _context;

        public EfFAQDal(InsureFlowAIDBEntities context) : base(context)
        {
            _context = context;
        }




    }
}
