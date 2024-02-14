using HotelProject.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.DataAccessLayer.Abstract
{
    public interface IBookingDal:IGenericDal<Booking>
    {

        int GetBookingCount();

        List<Booking> Last6Booking();

        void BookingStatusChangeApproved3(int id); //onaylama

        void BookingStatusChangeCancel(int id); // iptal etme

        void BookingStatusChangeWait(int id); // bekletme

    }
}
