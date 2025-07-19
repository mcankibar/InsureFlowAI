using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsureFlowAI.DAL.Abstract;
using InsureFlowAI.DAL.Models.DataModel;
using InsureFlowAI.DAL.Repositories;

namespace InsureFlowAI.DAL.EntityFramework
{
    public class EfServicesDal : GenericRepository<Tbl_Services>, IServicesDal
    {
        private readonly InsureFlowAIDBEntities _context;

        public EfServicesDal(InsureFlowAIDBEntities context) : base(context)
        {
            _context = context;
        }




    }
}
