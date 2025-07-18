using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsureFlowAI.DAL.Abstract;
using InsureFlowAI.DAL.DataModel;
using InsureFlowAI.DAL.Repositories;

namespace InsureFlowAI.DAL.EntityFramework
{
    public class EfCarouselDal : GenericRepository<Tbl_CarouselItems>, ICarouselDal
    {
        private readonly InsureFlowAIDBEntities _context;

        public EfCarouselDal(InsureFlowAIDBEntities context) : base(context)
        {
            _context = context;
        }

        


    }
}
