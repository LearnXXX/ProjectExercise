// See https://aka.ms/new-console-template for more information

//官网 https://www.questpdf.com/

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

