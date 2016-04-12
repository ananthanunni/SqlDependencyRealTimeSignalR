using SqlChangeTrackingPoc.Entity.Models;
using SqlChangeTrackingPoc.Models.DevTest;
using SqlChangeTrackingPoc.Repository.Contracts;
using SqlChangeTrackingPoc.Service.DependencyTracking;
using SqlChangeTrackingPoc.Service.Mappers;
using SqlChangeTrackingPoc.Service.Mappers.Contracts;
using SqlChangeTrackingPoc.UoW;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableDependency.Enums;
using TableDependency.EventArgs;

namespace SqlChangeTrackingPoc.Service
{
    public class DevTestService
    {
        private IRepository<DevTest> _devTestRepository = null;
        private UnitOfWork _uow = null;

        //TODO: DEPENDENCY INJECT IDevTestMapper.
        private IMapper<DevTest, DevTestModel> _mapper = null;
        private BaseDependencyTracker<DevTest> _dependencyTracker = null;


        private DevTestService()
        {
            // TODO: Make interface and do DI
            _mapper = new DevTestMapper();
            var dbContext = new SqlChangeTrackingPocDbContext();
            _uow = new UnitOfWork(dbContext);

            dbContext.Database.CreateIfNotExists();

            _devTestRepository = _uow.DevTestRepository;

            _dependencyTracker = new BaseDependencyTracker<DevTest>(dbContext.Database.Connection.ConnectionString, "DevTest");

            _dependencyTracker.ErrorOccurred += _dependencyTracker_ErrorOccurred;
            _dependencyTracker.RecordChanged += _dependencyTracker_RecordChanged;
            _dependencyTracker.BeginTracking();
        }

        private static DevTestService _instance = null;
        public static DevTestService Instance
        {
            get
            {
                return _instance ??(_instance = new DevTestService());
            }
        }

        public bool EventsSubscribed
        {
            get
            {
                return OnChanged != null && OnError != null;
            }
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

        public event EventHandler<RecordChangedEventArgs<DevTestModel>> OnChanged;

        private void _dependencyTracker_RecordChanged(object sender, TableDependency.EventArgs.RecordChangedEventArgs<DevTest> e)
        {
            var eventArg = new RecordChangedEventArgs<DevTestModel>(e,_mapper);

            if (OnChanged != null)
                OnChanged(sender, eventArg);
        }

        public event EventHandler<TableDependency.EventArgs.ErrorEventArgs> OnError;
        private void _dependencyTracker_ErrorOccurred(object sender, TableDependency.EventArgs.ErrorEventArgs e)
        {
            if (OnError != null)
                OnError(sender, e);
        }
    }

    public class RecordChangedEventArgs<T> : EventArgs
        where T :class
    {
        private IMapper<DevTest, DevTestModel> _mapper;
        public DevTestModel Record { get; set; }
        public ChangeType ChangeType { get; private set; }

        public RecordChangedEventArgs(IMapper<DevTest, DevTestModel> _mapper)
        {
            this._mapper = _mapper;
        }

        public RecordChangedEventArgs(TableDependency.EventArgs.RecordChangedEventArgs<DevTest> e, IMapper<DevTest, DevTestModel> _mapper)
        {
            this._mapper = _mapper;

            Record = _mapper.ConvertSingleFromEntity(e.Entity);
            ChangeType=e.ChangeType;            
        }
    }
}
