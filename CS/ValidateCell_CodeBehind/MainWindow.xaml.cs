using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ValidateCell_CodeBehind {
    public class Product {
        public string ProductName { get; set; }
        public double UnitPrice { get; set; }
        public bool Discontinued { get; set; }

        public Product(int i) {
            ProductName = "Product" + i;
            Discontinued = i % 5 == 0;
        }
    }
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            grid.ItemsSource = GetProductList().ToList();
        }
        static IEnumerable<Product> GetProductList() {
            Random r = new Random();
            return Enumerable.Range(0, 20).Select(i => new Product(i) { UnitPrice = r.Next(1, 50) });
        }

        void OnValidateCell(object sender, GridCellValidationEventArgs e) {
            if(e.Column.FieldName != nameof(Product.UnitPrice) || !((Product)e.Row).Discontinued)
                return;
            var cellValue = (double)e.CellValue;
            var discount = 100 - Convert.ToDouble(e.Value) / cellValue * 100;
            if(discount > 0 && discount <= 30)
                return;

            e.IsValid = false;
            e.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
            e.ErrorContent = discount < 0
                ? $"The price cannot be greater than {cellValue}"
                : $"The discount cannot be greater than 30% ({cellValue * 0.7}). Please correct the price.";
        }
    }
}
