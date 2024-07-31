// See https://aka.ms/new-console-template for more information



// 1、请确保您有资格使用社区许可证，不设置的话会报异常。
using QuestPDF.Infrastructure;
using QuestPDF;
using QuestPDFTest;
using QuestPDF.Fluent;

Settings.License = LicenseType.Community;

// 2、禁用QuestPDF库中文本字符可用性的检查
Settings.CheckIfAllTextGlyphsAreAvailable = false;

var reportCreator = new ReportDocument();
reportCreator.GeneratePdfAndShow();

// 3、PDF Document 创建
//var invoiceSourceData = CreateInvoiceDetails.GetInvoiceDetails();
//var document = new CreateInvoiceDocument(invoiceSourceData);

//// 4、生成 PDF 文件并在默认的查看器中显示
//document.GeneratePdfAndShow();

