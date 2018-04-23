using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using DevExpress.Xpf.Grid;
using System.Collections.ObjectModel;

namespace DXGrid_ValidatingEditors {
    public partial class Window1 : Window {
        public Window1() {
            InitializeComponent();
            this.DataContext = new MyViewModel();
        }

        private void GridColumn_Validate(object sender, GridCellValidationEventArgs e) {
            bool discontinued = ((Product)e.Row).Discontinued;
            if (discontinued) {
                double discount = 100 - (Convert.ToDouble(e.Value) * 100) / 
                                              Convert.ToDouble(e.CellValue);
                if (!(discount > 0 && discount <= 30)) {
                    e.IsValid = false;
                    e.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    if (discount < 0) {
                        e.ErrorContent = string.Format("The price cannot be greater than ${0}",
                                                    Convert.ToDouble(e.CellValue));
                        return;
                    }
                    e.ErrorContent = string.Format(
                       "The discount cannot be greater than 30% (${0}). Please correct the price.",
                       Convert.ToDouble(e.CellValue)*0.7);
                }
            }
        }

        private void TableView_HiddenEditor(object sender, EditorEventArgs e) {
            if (e.Column.FieldName != "Discontinued") return;
            grid.View.CommitEditing();
        }
    }


    public class MyViewModel {
        public MyViewModel() {
            CreateList();
        }

        public ObservableCollection<Product> ProductList { get; set; }
        void CreateList() {
            ProductList = new ObservableCollection<Product>();
            Random r = new Random();
            for (int i = 0; i < 20; i++) {
                Product p = new Product(i);
                p.UnitPrice = r.Next(1, 50);
                ProductList.Add(p);
            }
        }
    }
    public class Product {
        public Product(int i) {
            ProductName = "Product" + i;
            Discontinued = i % 5 == 0;
        }

        public string ProductName { get; set; }
        public int UnitPrice { get; set; }
        public bool Discontinued { get; set; }
    }
}
