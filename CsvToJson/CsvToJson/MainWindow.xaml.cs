namespace CsvToJson
{
    using System.ComponentModel;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// The property changed event handler.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// The back field for for <see cref="CsvText"/>
        /// </summary>
        private string mCsvText { get; set; }

        /// <summary>
        /// The back field for for <see cref="JsonText"/>
        /// </summary>
        private string mJsonText { get; set; }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or Sets the csv text.
        /// </summary>
        public string CsvText
        {
            get
            {
                return mCsvText;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    mCsvText = value;
                    OnPropertyChanged(nameof(CsvText));
                }
            }
        }

        /// <summary>
        /// Gets or Sets the json text.
        /// </summary>
        public string JsonText
        {
            get
            {
                return mJsonText;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    mJsonText = value;
                    OnPropertyChanged(nameof(JsonText));
                }
            }
        }

        #endregion

        #region Init

        /// <summary>
        /// The Main Window initialization.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            mCsvText = string.Empty;
            mJsonText = string.Empty;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Defines the actions after the click on the csv to Json buttton.
        /// </summary>
        /// <param name="csvTextRows">The csv rows.</param>
        /// <param name="indexRow">The index row.</param>
        public void CsvToJson(string[] csvTextRows, string[] indexRow)
        {
            JsonText = "[\r\n";

            for (int i = 1; i < csvTextRows.Length; i++)
            {
                string[] csvTextColumns = csvTextRows[i].Split(',');

                if (csvTextColumns.Length > indexRow.Length)
                {
                    // TODO: SHOW WARNING
                    break;
                }

                JsonText += "\t{\r\n";
                
                for (int j = 0; j < csvTextColumns.Length; j++)
                {
                    JsonText += $"\t\t\"{indexRow[j].Trim()}\" : \"{csvTextColumns[j].Trim()}\",\r\n";
                }

                if (i == csvTextRows.Length-1)
                {
                    JsonText += "\t}\r\n";
                }
                else
                {
                    JsonText += "\t},\r\n";
                }
            }

            JsonText += "]";
        }

        /// <summary>
        /// Updates the property.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Checks the csv format.
        /// </summary>
        private void CheckCsvFormat(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(CsvText))
            {
                // check how many values are in the first line and if the next lines has more, throw warning
                string[] csvTextRows = CsvText.Split("\r\n");
                string[] indexRow = csvTextRows[0].Split(',');

                CsvToJson(csvTextRows, indexRow);
            }
            else
            {
                // TODO: SHOW WARNING
            }
        }

        #endregion
    }
}
