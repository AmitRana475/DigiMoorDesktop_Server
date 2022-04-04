using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using WorkShipVersionII.Core;

namespace WorkShipVersionII.Control
{
    [TemplatePart(Name = "PART_MonthBack", Type = typeof(ButtonBase)),
    TemplatePart(Name = "PART_MonthForward", Type = typeof(ButtonBase)),
    TemplatePart(Name = "PART_Dates", Type = typeof(Selector))]
    public class DatePicker : System.Windows.Controls.Control, INotifyPropertyChanged
    {
        #region const
        const string CurrentlyViewedMonthYearPropertyName = "CurrentlyViewedMonthYear";
        const string CurrentlySelectedDateStringPropertyName = "CurrentlySelectedDateString";
        const string CurrentlyViewedMonthPropertyName = "CurrentlyViewedMonth";
        const string CurrentlyViewedYearPropertyName = "CurrentlyViewedYear";
        #endregion

        //buttons for the back and forward
        private ButtonBase backButton, forwardButton;
        private Selector datesList;

        //integer that stores the number of the month and year in view
        private int currentlyViewedMonth, currentlyViewedYear;


        // Gets the list of months
        public static IEnumerable<string> MonthsList
        {
            get
            {
                return DateHelper.MonthsList;
            }
        }


        // Gets or sets the current month in view
        public int CurrentlyViewedMonth
        {
            get { return currentlyViewedMonth; }
            set
            {
                currentlyViewedMonth = value;
                ChangeDate();
            }
        }


        // Gets or sets the current year in view
        public int CurrentlyViewedYear
        {
            get { return currentlyViewedYear; }
            set
            {
                currentlyViewedYear = value;
                ChangeDate();
            }
        }


        // returns the month currently selected as a full string
        public string CurrentlyViewedMonthYear
        {
            get
            {
                return String.Format("{0} {1}",
                    DateHelper.GetMonthDisplayName(currentlyViewedMonth),
                    currentlyViewedYear);
            }
        }





        /// Gets and sets the currently viewed date
        public DateTime CurrentlySelectedDate
        {
            get
            {
                return (DateTime)GetValue(CurrentlySelectedDateProperty);
            }
            set
            {
                SetValue(CurrentlySelectedDateProperty, value);
            }
        }


        // Gets and sets a string that represents the selected date
        private string currentlySelectedDateString;
        public string CurrentlySelectedDateString
        {
            get
            {
                var idl = datesList.SelectedItem != null ? ((DayCell)(datesList.SelectedItem)).IDLNumber : string.Empty;
                iDLNumbers = idl == string.Empty ? string.Empty : idl;
                idl = idl == string.Empty ? string.Empty : " (" + idl + ")";


                iDLNo = datesList.SelectedItem != null ? ((DayCell)(datesList.SelectedItem)).WID : Convert.ToDateTime(CurrentlySelectedDate1).ToString("dd");
                iDLNo = iDLNo.Replace("01", "1").Replace("02", "2").Replace("03", "3").Replace("04", "4").Replace("05", "5").Replace("06", "6").Replace("07", "7").Replace("08", "8").Replace("09", "9");// + idl1;
                OnPropertyChanged(new PropertyChangedEventArgs("IDLNo"));
                OnPropertyChanged(new PropertyChangedEventArgs("IDLNumbers"));

                return Convert.ToDateTime(CurrentlySelectedDate1).ToString("dd-MMM-yyyy") + idl;
            }
            set
            {
                currentlySelectedDateString = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentlySelectedDateString"));
            }
        }

        private string iDLNo;
        public string IDLNo
        {
            get
            {
                return iDLNo;
            }
            set
            {
                iDLNo = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IDLNo"));
            }
        }

        private string iDLNumbers;
        public string IDLNumbers
        {
            get
            {
                return iDLNumbers;
            }
            set
            {
                iDLNumbers = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IDLNumbers"));
            }
        }



        public object CurrentlySelectedDate1
        {
            get
            {
                return GetValue(CurrentlySelectedDateProperty);
            }
            set
            {
                SetValue(CurrentlySelectedDateProperty, value);
            }
        }

        // Gets and sets the currently viewed date
        public static readonly DependencyProperty CurrentlySelectedDateProperty =
            DependencyProperty.Register("CurrentlySelectedDate", typeof(DateTime), typeof(DatePicker),
            new UIPropertyMetadata(DateTime.Now, CurrentlySelectedDatePropertyChanged));

        //raise the property changed for CurrentlySelectedDateProperty
        static void CurrentlySelectedDatePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            DatePicker picker = (DatePicker)obj;
            picker.OnDateChanged(picker.CurrentlySelectedDate, (DateTime)e.OldValue);
            picker.OnPropertyChanged(new PropertyChangedEventArgs(CurrentlySelectedDateStringPropertyName));
        }


        // Event for the DateSelectionChanged raised when the date changes

        public static readonly RoutedEvent SelectedDateChangedEvent = EventManager.RegisterRoutedEvent("SelectedDateChanged",
            RoutingStrategy.Bubble, typeof(DateSelectedChangedEventHandler), typeof(DatePicker));


        // Event for the DateSelectionChanged raised when the date changes
        public event DateSelectedChangedEventHandler SelectedDateChanged
        {
            add { AddHandler(SelectedDateChangedEvent, value); }
            remove { RemoveHandler(SelectedDateChangedEvent, value); }
        }



        // Static constructor
        static DatePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DatePicker), new FrameworkPropertyMetadata(typeof(DatePicker)));
        }

        // Default constructor
        public DatePicker()
        {
            currentlyViewedMonth = DateTime.Now.Month;
            currentlyViewedYear = DateTime.Now.Year;
        }


        #region INotifyPropertyChanged Members

        /// <summary>
        /// Event raised when a property is changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Raises the property changed event
        /// </summary>
        /// <param name="e">The arguments to pass</param>

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        #endregion

        #region Overrides of control

        /// <summary>
        /// override to get the templated controls
        /// </summary>
        public override void OnApplyTemplate()
        {
            datesList = GetTemplateChild("PART_Dates") as Selector;
            backButton = GetTemplateChild("PART_MonthBack") as ButtonBase;
            forwardButton = GetTemplateChild("PART_MonthForward") as ButtonBase;

            backButton.Click += BackButtonClick;
            forwardButton.Click += ForwardButtonClick;
            datesList.SelectionChanged += DatesListSelectionChanged;
            ReBindListOfDays();
        }

        //on selected item cahnge of the selector control
        void DatesListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datesList.SelectedIndex != -1)
            {
                CurrentlySelectedDate = new DateTime(((DayCell)datesList.SelectedItem).YearNumber, ((DayCell)datesList.SelectedItem).MonthNumber, ((DayCell)datesList.SelectedItem).DayNumber);
                CurrentlySelectedDate1 = CurrentlySelectedDate;
                CurrentlySelectedDateString = CurrentlySelectedDate.ToString();
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentlySelectedDateString"));
            }
        }

        //rebinds to the dates
        public void ReBindListOfDays()
        {
            //Please note that the DateHelper.GetDaysOfMonth gets the days from a cache so it will not have a performance hit to call it everytime
            //int numberOfDaysFromPreviousMonth =
            //    (int)DateHelper.GetDayOfWeek(currentlyViewedYear, currentlyViewedMonth, 1);

            DayCell[] newDaysTemp = DateHelper.GetDaysOfMonth(currentlyViewedMonth, currentlyViewedYear);
            //get the last day of the month and determine the number of days to show from next month

            //int numberOfDaysFromNextMonth = 6 - (int)DateHelper.GetDayOfWeek(currentlyViewedYear, currentlyViewedMonth, newDaysTemp[newDaysTemp.Length - 1].DayNumber);


            DayCell[] newDays = new DayCell[newDaysTemp.Length + 0];
            int monthToGetNext;
            int yearTogetNext;
            //get the next month
            DateHelper.MoveMonthForward(currentlyViewedMonth, currentlyViewedYear, out monthToGetNext, out yearTogetNext);
            //get the data for next month
            DayCell[] nextDays = DateHelper.GetDaysOfMonth(monthToGetNext, yearTogetNext);//get the next month
            newDaysTemp.CopyTo(newDays, 0);//copy the new days array
            Array.Copy(nextDays, 0, newDays, newDaysTemp.Length, newDays.Length - newDaysTemp.Length);

            // DayCell[] listOfDays = new DayCell[numberOfDaysFromPreviousMonth + newDays.Length];
            DayCell[] listOfDays = new DayCell[newDays.Length];
            int monthToGetPrevious;
            int yearTogetPrevious;
            //move one month back
            DateHelper.MoveMonthBack(currentlyViewedMonth, currentlyViewedYear, out monthToGetPrevious, out yearTogetPrevious);
            DayCell[] oldDays = DateHelper.GetDaysOfMonth(monthToGetPrevious, yearTogetPrevious);//get the previous month
            //Array.Copy(oldDays, oldDays.Length - numberOfDaysFromPreviousMonth, listOfDays, 0, numberOfDaysFromPreviousMonth);
            //Array.Copy(newDays, 0, listOfDays, numberOfDaysFromPreviousMonth, newDays.Length);

            Array.Copy(oldDays, oldDays.Length - 0, listOfDays, 0, 0);
            Array.Copy(newDays, 0, listOfDays, 0, newDays.Length);

            //set the item source to the days to show
            datesList.ItemsSource = listOfDays;
        }

        //moves the month currently being viewed backward
        void BackButtonClick(object sender, RoutedEventArgs e)
        {
            //move the month back
            DateHelper.MoveMonthBack(currentlyViewedMonth, currentlyViewedYear, out currentlyViewedMonth, out currentlyViewedYear);
            ChangeDate();
        }

        //changes the current date
        void ChangeDate()
        {
            ReBindListOfDays();

            OnPropertyChanged(new PropertyChangedEventArgs(CurrentlyViewedYearPropertyName));
            OnPropertyChanged(new PropertyChangedEventArgs(CurrentlyViewedMonthPropertyName));
            OnPropertyChanged(new PropertyChangedEventArgs(CurrentlyViewedMonthYearPropertyName));
        }

        protected void OnDateChanged(DateTime newDate, DateTime oldDate)
        {
            DateSelectedChangedRoutedEventArgs args =
                new DateSelectedChangedRoutedEventArgs(SelectedDateChangedEvent);
            args.NewDate = newDate;
            args.OldDate = oldDate;
            RaiseEvent(args);
        }

        //moves the month currently view forward
        void ForwardButtonClick(object sender, RoutedEventArgs e)
        {
            DateHelper.MoveMonthForward(currentlyViewedMonth, currentlyViewedYear, out currentlyViewedMonth, out currentlyViewedYear);
            ChangeDate();
        }

        #endregion
    }


    // Object to represent a single day as a cell
    public class DayCell
    {

        readonly string wID;
        public string WID
        {
            get { return wID; }
        }

        readonly string iDLNumber;
        public string IDLNumber
        {
            get { return iDLNumber; }
        }

        // gets the day number for the cell
        readonly int dayNumber;
        public int DayNumber
        {
            get { return dayNumber; }
        }
        readonly string dateID;
        public string DateID
        {
            get { return dateID; }
        }



        // gets the month number for the cell
        readonly int monthNumber;
        public int MonthNumber
        {
            get { return monthNumber; }
        }

        // gets the year number for the cell
        readonly int yearNumber;
        public int YearNumber
        {
            get { return yearNumber; }
        }


        // Constructor
        public DayCell(int day, int month, int year, string IDLNumbers, string WIDs, string DateID)
        {
            dayNumber = day;
            monthNumber = month;
            yearNumber = year;
            iDLNumber = IDLNumbers;
            wID = WIDs;
            dateID = DateID;
        }
    }



  



    // Routed event args for the DateSelectedChanged
    public class DateSelectedChangedRoutedEventArgs : RoutedEventArgs
    {

        // Constructor for the event args
        public DateSelectedChangedRoutedEventArgs(RoutedEvent routedEvent)
            : base(routedEvent) { }


        // Gets or sets the new date that was set
        public DateTime NewDate { get; set; }

        // Gets or sets the old date that was set
        public DateTime OldDate { get; set; }
    }


    // Delegate for the DateSelectedChanged event
    public delegate void DateSelectedChangedEventHandler(object sender, DateSelectedChangedRoutedEventArgs e);



    // Converter used to compare 2 months

    public class IsCurrentMonthConverter : OneWayMultiValueConverter
    {

        // Compares 2 months together
        //Returns true if there is a match between the 2 months
        public override object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0] == null)
                return false;

            int currentlyViewedMonth = (int)values[0];
            int otherMonth = (int)values[1];

            return currentlyViewedMonth == otherMonth;
            //return otherMonth;
        }
    }


    /// <summary>
    /// converter to calculate the size for the cell of the calender
    /// </summary>
    public class CellSizeConverter : OneWayValueConverter
    {
        const int daysToFitHorizontal = 7;
        const double minimumValue = 10;//the minum size to return

        #region IValueConverter Members

        /// <summary>
        /// Converter for the calender control to measure the widths to calculate
        /// </summary>
        /// <param name="value">Pass the Actual width of the parent control</param>
        /// <param name="targetType">Target type</param>
        /// <param name="parameter">Pass widthCell to calculate the width a particular cell.
        /// Pass widthCellContainer to calculate the witdth of the parent control</param>
        /// <param name="culture">The current culture in use</param>
        /// <returns>Returns the new width to use</returns>
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double valuePassed = (double)value;

            if (parameter != null && !Double.IsNaN(valuePassed))
            {
                if (parameter.ToString() == "widthCell")
                {
                    return Math.Max(valuePassed / daysToFitHorizontal, minimumValue) - 2;
                }

                if (parameter.ToString() == "widthCellContainer")
                {
                    return Math.Max(valuePassed - 10, minimumValue);
                }
            }
            return 20.0;
        }

        #endregion
    }

    /// <summary>
    /// converts the date string to the value
    /// </summary>
    public class MonthConverter : IValueConverter
    {
        #region IValueConverter Members

        /// <summary>
        /// Converts the month from a number to the actual string
        /// </summary>
        /// <param name="value">The value as integer</param>
        /// <param name="targetType">Target type</param>
        /// <param name="parameter">The parameter to use</param>
        /// <param name="culture">The current culture in use</param>
        /// <returns>Returns the selected item to select for the drop down list</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((Months)(int)value).ToString();
        }

        /// <summary>
        /// Converts the value back from ComboBoxitem to a number that can be set for the current month
        /// </summary>
        /// <param name="value">The comboBoxItem Selected</param>
        /// <param name="targetType">Target type</param>
        /// <param name="parameter">The parameter to use</param>
        /// <param name="culture">The current culture in use</param>
        /// <returns>Returns a number that represents the month selected</returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (int)Enum.Parse(typeof(Months), value.ToString());
        }

        #endregion
    }

    /// <summary>
    /// Validation rule for the Year
    /// </summary>
    public class YearValidation : ValidationRule
    {
        /// <summary>
        /// Validation for the year
        /// </summary>
        /// <param name="value">The year value</param>
        /// <param name="cultureInfo">The culture info</param>
        /// <returns>Returns the validation result</returns>
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            int year;
            if (int.TryParse((string)value, out year))
                if (year > 0 && year <= 9999)
                    return new ValidationResult(true, null);
            return new ValidationResult(false, null);
        }
    }
}
