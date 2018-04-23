using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace DXPivotGrid_SaveRestoreLayout {
    public partial class MainPage : UserControl {
        string dataFileName = "DXPivotGrid_SaveRestoreLayout.nwind.xml";
        MemoryStream stream;
        public MainPage() {
            InitializeComponent();

            // Parses an XML file and creates a collection of data items.
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream(dataFileName);
            XmlSerializer s = new XmlSerializer(typeof(OrderData));
            object dataSource = s.Deserialize(stream);

            // Binds a pivot grid to this collection.
            pivotGridControl1.DataSource = dataSource;
        }
        private void buttonSave_Click(object sender, RoutedEventArgs e) {
            stream = new MemoryStream();
            pivotGridControl1.SaveLayoutToStream(stream);
        }
        private void buttonLoad_Click(object sender, RoutedEventArgs e) {
            if (stream == null) return;
            stream.Position = 0;
            pivotGridControl1.RestoreLayoutFromStream(stream);
        }
    }
}