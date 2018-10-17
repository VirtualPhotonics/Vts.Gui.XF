using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Vts.Extensions;
using Vts.Gui.XF.Model;

namespace Vts.Gui.XF.ViewModel
{
    /// <summary>
    /// View model exposing Enum choices with change notification.
    /// Xamarin Forms does not allow xaml to bind to Generic Types (e.g. Tvalue) so
    /// SelectedIndex has been added to bind to in the xaml.
    /// 
    /// </summary>
    public class OptionViewModel<TValue> : BindableObject
    {
        private bool _enableMultiSelect;
        private string _groupName;
        private Dictionary<TValue, OptionModel<TValue>> _options;
        private string _selectedDisplayName;
        private string[] _selectedDisplayNames;
        private int _selectedIndex;
        private TValue _selectedValue;
        private int[] _selectedIndexes;
        private TValue[] _selectedValues;
        private bool _showTitle;
        private string[] _unSelectedDisplayNames;
        private int[] _unSelectedIndexes;
        private TValue[] _unSelectedValues;

        public OptionViewModel(string groupName, bool showTitle, TValue initialValue, TValue[] allValues,
            bool enableMultiSelect = false)
        {
            ShowTitle = showTitle;
            GroupName = groupName;
            _enableMultiSelect = enableMultiSelect;

            // todo: CreateAvailableOptions should be owned by either this class or an OptionModelService class
            Options = OptionModel<TValue>.CreateAvailableOptions(OnOptionPropertyChanged, groupName, initialValue,
                allValues, _enableMultiSelect);

            SelectedValue = initialValue;

            UpdateOptionsNamesAndValues();
        }

        public OptionViewModel(string groupName, bool showTitle, TValue[] allValues)
            : this(groupName, showTitle, default(TValue), allValues)
        {
        }

        public OptionViewModel(string groupName, bool showTitle, TValue initialValue)
            : this(groupName, showTitle, initialValue, null)
        {
        }

        public OptionViewModel(string groupName, TValue initialValue)
            : this(groupName, true, initialValue, null)
        {
        }

        public OptionViewModel(string groupName, TValue[] allValues)
            : this(groupName, true, default(TValue), allValues)
        {
        }

        public OptionViewModel(string groupName, bool showTitle)
            : this(groupName, showTitle, default(TValue), null)
        {
        }

        public OptionViewModel(string groupName)
            : this(groupName, true, default(TValue), null)
        {
        }

        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                _selectedIndex = value;
                if (AllValues != null)
                {
                    SelectedValue = AllValues[value];
                }
                OnPropertyChanged("SelectedIndex");
            }
        }

        public TValue SelectedValue
        {
            get { return _selectedValue; }
            set
            {
                _selectedValue = value;
                OnPropertyChanged("SelectedValue");
            }
        }

        public string SelectedDisplayName
        {
            get { return _selectedDisplayName; }
            set
            {
                _selectedDisplayName = value;
                OnPropertyChanged("SelectedDisplayName");
            }
        }

        public bool ShowTitle
        {
            get { return _showTitle; }
            set
            {
                _showTitle = value;
                OnPropertyChanged("ShowTitle");
            }
        }

        public string GroupName
        {
            get { return _groupName; }
            set
            {
                _groupName = value;
                OnPropertyChanged("GroupName");
            }
        }

        public bool EnableMultiSelect
        {
            get { return _enableMultiSelect; }
            set
            {
                _enableMultiSelect = value;
                OnPropertyChanged("EnableMultiSelect");
            }
        }

        public int[] SelectedIndexes
        {
            get { return _selectedIndexes; }
            set
            {
                _selectedIndexes = value;
                if (AllValues != null)
                {
                    var i = 0;
                    foreach (var index in _selectedIndexes)
                    {
                        SelectedValues[i] = AllValues[index];
                        i++;
                    }
                }
                OnPropertyChanged("SelectedIndexes");
            }
        }

        // todo: created this in parallel with SelectedValue, so as not to break other code. need to merge functionality across codebase to use this version
        public TValue[] SelectedValues
        {
            get { return _selectedValues; }
            set
            {
                _selectedValues = value;
                OnPropertyChanged("SelectedValues");
            }
        }

        public string[] SelectedDisplayNames
        {
            get { return _selectedDisplayNames; }
            set
            {
                _selectedDisplayNames = value;
                OnPropertyChanged("SelectedDisplayNames");
            }
        }

        public int[] UnSelectedIndexes
        {
            get { return _unSelectedIndexes; }
            set
            {
                _unSelectedIndexes = value;
                if (AllValues != null)
                {
                    var i = 0;
                    foreach (var index in _unSelectedIndexes)
                    {
                        UnSelectedValues[i] = AllValues[index];
                        i++;
                    }
                }
                OnPropertyChanged("UnSelectedIndexes");
            }
        }

        public TValue[] UnSelectedValues
        {
            get { return _unSelectedValues; }
            set
            {
                _unSelectedValues = value;
                OnPropertyChanged("UnSelectedValues");
            }
        }

        public string[] UnSelectedDisplayNames
        {
            get { return _unSelectedDisplayNames; }
            set
            {
                _unSelectedDisplayNames = value;
                OnPropertyChanged("UnSelectedDisplayNames");
            }
        }
        // CKH: AllDisplayNames added to support used of picker since no dynamic xaml available in XF
        public string[] AllDisplayNames { get; set; }
        public TValue[] AllValues { get; set; }

        public Dictionary<TValue, OptionModel<TValue>> Options
        {
            get { return _options; }
            set
            {
                _options = value;
                OnPropertyChanged("Options");
            }
        }

        private void OnOptionPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var option = sender as OptionModel<TValue>;
            if (option.IsSelected)
            {
                SelectedValue = option.Value;
                SelectedIndex = option.ID;
                SelectedDisplayName = option.DisplayName;
            }

            UpdateOptionsNamesAndValues();

            if (e.PropertyName != "IsEnabled" && EnableMultiSelect && Options != null)
            {
                var MIN_CHOICES = 1;
                var MAX_CHOICES = 2;
                var numSelected = (from o in _options where o.Value.IsSelected select o).Count();

                // disable the unselected choices beyond a MAX_CHOICES number of concurrent multi-select options
                Options.Where(o => !o.Value.IsSelected).ForEach(o => o.Value.IsEnabled = numSelected < MAX_CHOICES);

                // if there is only MIN_CHOICES selected choice in multi-select mode, disable others from being further unselected
                Options.Where(o => o.Value.IsSelected).ForEach(o => o.Value.IsEnabled = numSelected > MIN_CHOICES);
            }
        }

        private void UpdateOptionsNamesAndValues()
        {
            if (_options == null)
                return;

            // todo: created these in parallel with SelectedValue, so as not to break other code. need to merge functionality across codebase to use SelectedValues
            var selectedOptions = (from o in _options where o.Value.IsSelected select o).ToArray();
            var unSelectedOptions = (from o in _options where !o.Value.IsSelected select o).ToArray();

            // update arrays and explicitly fire property changed, so we don't trip on intermediate changes 
            _selectedValues = selectedOptions.Select(item => item.Value.Value).ToArray();
            _selectedDisplayNames = selectedOptions.Select(item => item.Value.DisplayName).ToArray();
            _unSelectedValues = unSelectedOptions.Select(item => item.Value.Value).ToArray();
            _unSelectedDisplayNames = unSelectedOptions.Select(item => item.Value.DisplayName).ToArray();
            AllDisplayNames = _selectedDisplayNames.Concat(_unSelectedDisplayNames).ToArray();
            AllValues = _selectedValues.Concat(_unSelectedValues).ToArray();
            OnPropertyChanged("SelectedValues");
            OnPropertyChanged("SelectedDisplayNames");
            OnPropertyChanged("UnSelectedValues");
            OnPropertyChanged("UnSelectedDisplayNames");
        }
    }
}