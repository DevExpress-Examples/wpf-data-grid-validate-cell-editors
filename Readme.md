<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128653794/21.2.2%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E1592)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# WPF Data Grid - How to Validate Cell Editors

This example shows how to validate the focused cell's value. In this example, users cannot reduce the product's price by more than 30% if the product is on discount. 

![](https://docs.devexpress.com/WPF/images/GridViewBase_ValidateCellCommand.png?v=21.2)

<!-- default file list -->

## Files to Look At

### Code Behind Technique

- [MainWindow.xaml](./CS/ValidateCell_CodeBehind/MainWindow.xaml) ([MainWindow.xaml](./VB/ValidateCell_CodeBehind/MainWindow.xaml))
- [MainWindow.xaml.cs](./CS/ValidateCell_CodeBehind/MainWindow.xaml.cs#L28-L41) ([MainWindow.xaml.vb](./VB/ValidateCell_CodeBehind/MainWindow.xaml.vb#L36-L44))

### MVVM Technique

- [MainWindow.xaml](./CS/ValidateCell_MVVM/MainWindow.xaml) ([MainWindow.xaml](./VB/ValidateCell_MVVM/MainWindow.xaml))
- [MainViewModel.cs](./CS/ValidateCell_MVVM/MainViewModel.cs#L31-L44) ([MainViewModel.vb](./VB/ValidateCell_MVVM/MainViewModel.vb#L39-L47))

<!-- default file list end -->

## Documentation

- [Data Editing and Validation](https://docs.devexpress.com/WPF/6108/controls-and-libraries/data-grid/data-editing-and-validation)
- [Cell Validation](https://docs.devexpress.com/WPF/6113/controls-and-libraries/data-grid/data-editing-and-validation/input-validation/cell-validation)
- [GridViewBase.ValidateCell](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.GridViewBase.ValidateCell)
- [TreeListView.ValidateCell](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TreeListView.ValidateCell)
- [GridViewBase.ValidateCellCommand](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.GridViewBase.ValidateCellCommand)
- [TreeListView.ValidateCellCommand](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TreeListView.ValidateCellCommand)

## More Examples

- [How to Validate Data Rows](https://github.com/DevExpress-Examples/how-to-validate-data-rows-e1593)
- [How to Implement Attributes-Based Validation](https://github.com/DevExpress-Examples/how-to-implement-attributes-based-validation-e3191)
