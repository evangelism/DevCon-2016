using Microsoft.AspNetCore.Mvc;
using MyShuttle.Data;
using MyShuttle.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace MyShuttle.API
{

    [NoCacheFilter]
    public class AnalyticsController : Controller
    {
        private const int DefaultCarrierID = 0;

        IDriverRepository _driverRepository;

        IVehicleRepository _vehicleRepository;
        ICarrierRepository _carrierRepository;
        IRidesRepository _ridesRepository;

        public AnalyticsController(IDriverRepository driverRepository
            , IVehicleRepository vehicleRepository, ICarrierRepository carrierRepository, IRidesRepository ridesRepository)
        {
            _driverRepository = driverRepository;

            _vehicleRepository = vehicleRepository;
            _carrierRepository = carrierRepository;
            _ridesRepository = ridesRepository;
        }

        [ActionName("topdrivers")]
        public async Task<ICollection<Driver>> GetTopDrivers()
        {
            return await _driverRepository.GetTopDriversAsync(DefaultCarrierID, GlobalConfig.TOP_NUMBER);
        }

        [ActionName("topvehicles")]
        public async Task<ICollection<Vehicle>> GetTopVehicles()
        {
            return await _vehicleRepository.GetTopVehiclesAsync(DefaultCarrierID, GlobalConfig.TOP_NUMBER);
        }

        [ActionName("summary")]
        public async Task<SummaryAnalyticInfo> GetSummaryInfo()
        {
            return await _carrierRepository.GetAnalyticSummaryInfoAsync(DefaultCarrierID);
        }

        [ActionName("rides")]
        public async Task<RidesAnalyticInfo> GetRidesEvolution()
        {
            DateTime fromFilter = DateTime.UtcNow.AddDays(-30);

            var labels = new List<int>();
            var to = DateTime.UtcNow;

            var date = fromFilter;
            while (DateTime.Compare(date, to) < 0)
            {
                labels.Add(date.Day);
                date = date.AddDays(1);
            }

            int carrierId = DefaultCarrierID;

            IEnumerable<RideResult> evolution = await _ridesRepository.GetRidesEvolutionAsync(carrierId, fromFilter);

            var values = new int[labels.Count];
            foreach (var item in evolution)
            {
                int index = (int)(item.Date - fromFilter).TotalDays;
                values[index] = item.Value;
            }

            RideGroupInfo rides = new RideGroupInfo()
            {
                Days = labels,
                Values = values
            };

            int ridesCount = await _ridesRepository.LastDaysRidesAsync(carrierId, fromFilter);
            int passengersCount = await _ridesRepository.LastDaysPassengersAsync(carrierId, fromFilter);

            return new RidesAnalyticInfo()
            {
                LastDaysRides = ridesCount,
                LastDaysPassengers = passengersCount,
                RidesEvolution = rides
            };
        }
    }
}