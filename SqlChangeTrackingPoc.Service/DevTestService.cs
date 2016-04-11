using SqlChangeTrackingPoc.Entity.Models;
using SqlChangeTrackingPoc.Models.DevTest;
using SqlChangeTrackingPoc.Repository.Contracts;
using SqlChangeTrackingPoc.Service.Mappers;
using SqlChangeTrackingPoc.Service.Mappers.Contracts;
using SqlChangeTrackingPoc.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlChangeTrackingPoc.Service
{
    public class DevTestService
    {
        private IRepository<DevTest> _devTestRepository = null;
        private UnitOfWork _uow = null;

        //TODO: DEPENDENCY INJECT IDevTestMapper.
        private IMapper<DevTest, DevTestModel> _mapper = null;

        public DevTestService()
        {
            // TODO: Make interface and do DI
            _mapper = new DevTestMapper();
            _uow = new UnitOfWork(new SqlChangeTrackingPocDbContext());

            _devTestRepository = _uow.DevTestRepository;
        }

        public IEnumerable<DevTestModel> GetAllDevTests()
        {
            var result = _devTestRepository.Get();

            return _mapper.ConvertMultipleFromEntityCollection(result);
        }

        public void Add(DevTestModel model)
        {
            _devTestRepository.Add(_mapper.ConvertSingleToEntity(model));
            _uow.Commit();
        }

        public void Delete(int id)
        {
            var item = _devTestRepository.Find(id);
            _devTestRepository.Remove(item);
            _uow.Commit();
        }

        public DevTestModel GetItem(int id)
        {
            var item = _devTestRepository.Find(id);

            return _mapper.ConvertSingleFromEntity(item);
        }

        public void Update(int id, DevTestModel model)
        {
            var item = _devTestRepository.Find(id);

            item.AffiliateName = model.AffiliateName;
            item.CampaignName = model.CampaignName;
            item.Clicks = model.Clicks;
            item.Conversions = model.Conversions;
            item.Date = model.Date;
            item.Impressions = model.Impressions;

            _uow.Commit();
        }
    }
}
