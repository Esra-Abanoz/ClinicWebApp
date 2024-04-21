using System.Diagnostics;
namespace ClinicWeb.Common.GeneralResponse
{
    public abstract class BaseSearchModel : BaseRequestModel
    {
        [DebuggerStepThrough]
        protected BaseSearchModel()
        {
            page = 1;
            limit = 20;
        }
        private int _page;
        public int page
        {
            get
            {
                if (_skip.HasValue && _take.HasValue)
                {
                    return (_skip.Value / _take.Value) + 1;
                }
                return _page;
            }
            set
            {
                _page = value;
                _skip = value * limit;
            }
        }
        private int _limit;
        public int limit
        {
            get
            {
                if (_take.HasValue)
                {
                    return _take.Value;
                }
                return _limit;
            }
            set
            {
                _limit = value;
                _take = value;
            }
        }
        #region DexExtremeParameters
        private int? _skip;
        public int? skip
        {
            get
            {
                if (!_skip.HasValue)
                {
                    return page * limit;
                }
                return _skip;
            }
            set
            {
                _skip = value;
            }
        }
        private int? _take;
        public int? take
        {
            get
            {
                if (!_take.HasValue)
                {
                    return limit;
                }
                return _take;
            }
            set
            {
                _take = value;
            }
        }
        #endregion
    }
}
