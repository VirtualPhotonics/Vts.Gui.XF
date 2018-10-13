using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Vts.Extensions;
using Vts.Gui.XF.Model;

namespace Vts.Gui.XF.ViewModel
{
    // todo: consider refactoring to derive from ObservableCollection:
    // http://www.thejoyofcode.com/ViewModels_and_CheckListBoxes.aspx

    /// <summary>
    ///     View model exposing Enum choices with change notification
    /// </summary>
    public class OptionViewModel<TValue> : BindableObject
    {
        private bool _enableMultiSelect;
        private string _GroupName;
        private Dictionary<TValue, OptionModel<TValue>> _Options;
        private string _SelectedDisplayName;
        private string[] _selectedDisplayNames;
        private int _selectedIndex;
        private TValue _SelectedValue;
        private TValue[] _selectedValues;
        private bool _ShowTitle;
        private string[] _unSelectedDisplayNames;
        private TValue[] _unSelectedValues;
        // CKH: added following to allow dynamic xaml
        private string[] _allDisplayNames;
        private TValue[] _allValues;

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
            get { return _SelectedValue; }
            set
            {
                _SelectedValue = value;
                OnPropertyChanged("SelectedValue");
            }
        }

        public string SelectedDisplayName
        {
            get { return _SelectedDisplayName; }
            set
            {
                _SelectedDisplayName = value;
                OnPropertyChanged("SelectedDisplayName");
            }
        }

        public bool ShowTitle
        {
            get { return _ShowTitle; }
            set
            {
                _ShowTitle = value;
                OnPropertyChanged("ShowTitle");
            }
        }

        public string GroupName
        {
            get { return _GroupName; }
            set
            {
                _GroupName = value;
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
        public string[] AllDisplayNames
        {
            get { return _allDisplayNames; }
            set
            {
                _allDisplayNames = value;
            }
        }
        public TValue[] AllValues
        {
            get { return _allValues; }
            set
            {
                _allValues = value;
            }
        }

        public Dictionary<TValue, OptionModel<TValue>> Options
        {
            get { return _Options; }
            set
            {
                _Options = value;
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
                var numSelected = (from o in _Options where o.Value.IsSelected select o).Count();

                // disable the unselected choices beyond a MAX_CHOICES number of concurrent multi-select options
                Options.Where(o => !o.Value.IsSelected).ForEach(o => o.Value.IsEnabled = numSelected < MAX_CHOICES);

                // if there is only MIN_CHOICES selected choice in multi-select mode, disable others from being further unselected
                Options.Where(o => o.Value.IsSelected).ForEach(o => o.Value.IsEnabled = numSelected > MIN_CHOICES);
            }
        }

        private void UpdateOptionsNamesAndValues()
        {
            if (_Options == null)
                return;

            // todo: created these in parallel with SelectedValue, so as not to break other code. need to merge functionality across codebase to use SelectedValues
            var selectedOptions = (from o in _Options where o.Value.IsSelected select o).ToArray();
            var unSelectedOptions = (from o in _Options where !o.Value.IsSelected select o).ToArray();

            // update arrays and explicitly fire property changed, so we don't trip on intermediate changes 
            _selectedValues = selectedOptions.Select(item => item.Value.Value).ToArray();
            _selectedDisplayNames = selectedOptions.Select(item => item.Value.DisplayName).ToArray();
            _unSelectedValues = unSelectedOptions.Select(item => item.Value.Value).ToArray();
            _unSelectedDisplayNames = unSelectedOptions.Select(item => item.Value.DisplayName).ToArray();
            _allDisplayNames = _selectedDisplayNames.Concat(_unSelectedDisplayNames).ToArray();
            _allValues = _selectedValues.Concat(_unSelectedValues).ToArray();
            OnPropertyChanged("SelectedValues");
            OnPropertyChanged("SelectedDisplayNames");
            OnPropertyChanged("UnSelectedValues");
            OnPropertyChanged("UnSelectedDisplayNames");
        }
    }
}