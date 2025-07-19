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
    public class CarouselManager : ICarouselService
    {
        private readonly ICarouselDal _carouselDal;

        public CarouselManager(ICarouselDal carouselDal)
        {
            _carouselDal = carouselDal;
        }

        public void TInsert(Tbl_CarouselItems entity)
        {
            throw new NotImplementedException();
        }

        public void TDelete(int id)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(Tbl_CarouselItems entity)
        {
            throw new NotImplementedException();
        }

        public List<Tbl_CarouselItems> TGetAll()
        {
            return _carouselDal.GetAll().ToList();
        }

        public Tbl_CarouselItems TGetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
