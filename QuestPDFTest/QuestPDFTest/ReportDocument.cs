using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using static System.Net.Mime.MediaTypeNames;

namespace QuestPDFTest
{
    class ReportData
    {
        public string 通貨ペア { get; set; }

        //新規
        public string 新規区分 { get; set; }
        public DateTime 新規約定日時 { get; set; }
        public string 新規売 { get; set; }
        public string 新規買 { get; set; }
        public double 新規約定価格 { get; set; }

        //決済
        public string 決済区分 { get; set; }
        public DateTime 決済約定日時 { get; set; }
        public string 決済売 { get; set; }
        public string 決済買 { get; set; }
        public double 決済約定価格 { get; set; }
        public string 決済換算レート { get; set; }
        public DateTime 決済受渡日 { get; set; }

        public double SwapPoint { get; set; }
        public double 差損益金 { get; set; }
        public double 手数料 { get; set; }
        public double 小計 { get; set; }
    }

    class PositionReport
    {
        public string 通貨ペア { get; set; }
        public string 売 { get; set; }
        public string 買 { get; set; }
        public DateTime 当初約定日時 { get; set; }
        public double 当初約定価格 { get; set; }
        public double 最新ロール値 { get; set; }
        public double 評価レート { get; set; }
        public double 評価損益 { get; set; }
        public double 累積SwapPoint { get; set; }
        public double 累積ロール損益 { get; set; }
    }

    internal class ReportDocument : IDocument
    {
        private int defaultFontSize = 7;
        public void Compose(IDocumentContainer container)
        {
            container
                 .Page(page =>
                 {
                     page.Size(PageSizes.A4.Height, PageSizes.A4.Width);
                     //设置页面的边距
                     page.Margin(15);

                     //字体默认大小18号字体
                     page.DefaultTextStyle(x =>
                     {
                         x.FontSize(defaultFontSize);
                         //x.FontFamily("HeiseiMin-W3");
                         return x;
                     });

                     //页眉部分
                     page.Header().Element(BuildHeaderInfo);

                     //内容部分
                     page.Content().Element(BuildContentInfo);

                     ////页脚部分
                     //page.Footer().AlignCenter().Text(text =>
                     //{
                     //    text.CurrentPageNumber();
                     //    text.Span(" / ");
                     //    text.TotalPages();
                     //});
                 });
        }

        void BuildContentInfo(IContainer container)
        {
            container.Column(column =>
            {
                //column.Item().AlignCenter().Text("取引報告書兼証拠金受領書兼取引残高報告書").FontSize(12).FontFamily("HeiseiMin-W3").Bold();

                column.Item().Element(CreateTitle);
                column.Item().Element(CreateKouzaInfo);

                column.Item().Text("お取引明細").FontSize(defaultFontSize);
                column.Item().Element(CreateTransactionDetails);

                column.Item().Text("").FontSize(defaultFontSize);
                column.Item().Text("入出金明細").FontSize(defaultFontSize);
                column.Item().Element(DepositWithdrawalDetails);

                column.Item().Text("").FontSize(defaultFontSize);
                column.Item().Text("お預り額").FontSize(defaultFontSize);
                column.Item().Element(DepositAmountDetails);

                column.Item().Text("").FontSize(defaultFontSize);
                column.Item().Text("保有ポジション").FontSize(defaultFontSize);
                column.Item().Element(HoldingPositionDetails);
                column.Item().Text("※評価レート　上記期間末尾の終値です。").FontSize(defaultFontSize);
                //column.Spacing(20);

                column.Item().Text("・お客様が支払うこととなる金銭の額及び計算方法について").FontSize(defaultFontSize);
                column.Item().Text("①\tスワップポイントはロールオーバーする度に加算されるもので、お客様が高金利通貨を買った場合にはお受取り、逆に高金利通貨を売った場合にはお支払いいただくこととなります。").FontSize(defaultFontSize);
                column.Item().Text("      々のスワップポイントは2カ国間の金利差等により変動する為、正確な算出式の記載は困難ですが、スワップ履歴画面で日々の実績値をご確認いただけます。").FontSize(defaultFontSize);
                column.Item().Text("②\t差損益金・・・新規約定価格と決済約定価格の差額に取引単位及び数量を乗じたもの。").FontSize(defaultFontSize);
                column.Item().Text("③\t手数料・・・新規並びに決済の取引に係る手数料は無料です。").FontSize(defaultFontSize);
                column.Item().Text("④\t小計＝（スワップポイント）＋（差損益金）-（手数料）").FontSize(defaultFontSize);
                column.Item().Text("⑤\t評価損益・・・最新ロール値と評価レートの差額に取引単位及び数量を乗じたもの。").FontSize(defaultFontSize);
                column.Item().Text("⑥\t累積スワップポイント・・・当初約定以来から対象期間末尾までのスワップポイントの累積額です。").FontSize(defaultFontSize);
                column.Item().Text("⑦\t累積ロール損益・・・当初約定価格と最新ロール値の差額に取引単位及び数量を乗じたもの。").FontSize(defaultFontSize);

            });
        }

        /// <summary>
        /// 保有ポジション
        /// </summary>
        /// <param name="container"></param>
        void HoldingPositionDetails(IContainer container)
        {

            var datas = new List<PositionReport> {
                new PositionReport{通貨ペア="米ﾄﾞﾙ/円",売="1", 買="",当初約定日時=DateTime.Now,最新ロール値=161.28,評価レート=161.28,評価損益=0,累積SwapPoint=-12345,累積ロール損益=-78945},
                new PositionReport{通貨ペア="ﾕｰﾛ/円",売="1", 買="",当初約定日時=DateTime.Now,最新ロール値=161.28,評価レート=161.28,評価損益=0,累積SwapPoint=-12345,累積ロール損益=-78945},
                new PositionReport{通貨ペア="英ﾎﾟﾝﾄﾞ/円",売="", 買="1",当初約定日時=DateTime.Now,最新ロール値=161.28,評価レート=161.28,評価損益=0,累積SwapPoint=-12345,累積ロール損益=-78945},
                new PositionReport{通貨ペア="豪ﾄﾞﾙ/米ﾄﾞﾙ",売="", 買="1",当初約定日時=DateTime.Now,最新ロール値=161.28,評価レート=161.28,評価損益=0,累積SwapPoint=-12345,累積ロール損益=-78945},
            };
            IContainer DefaultCellStyle(IContainer container, string backgroundColor)
            {
                return container
                    .Border((float)0.2)
                    .BorderColor(Colors.Black)
                    .Background(backgroundColor)
                    //.PaddingVertical(5)
                    //.PaddingHorizontal(10)
                    .AlignCenter()
                    .AlignMiddle()
                    .DefaultTextStyle(x => x.FontSize(defaultFontSize))
                    ;
            }

            container.Table(table =>
            {
                IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White);
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(80);//通貨ペア

                    columns.ConstantColumn(30);//売
                    columns.ConstantColumn(30);//買
                    columns.ConstantColumn(80);//当初約定日時
                    columns.ConstantColumn(50);//当初約定価格
                    columns.ConstantColumn(50);//最新ロール値
                    columns.ConstantColumn(50);//評価レート※
                    columns.ConstantColumn(80);//評価損益
                    columns.ConstantColumn(130);//累積スワップポイント
                    columns.ConstantColumn(130);//累積ロール損益
                });

                table.Header(header =>
                {
                    // please be sure to call the 'header' handler!

                    header.Cell().Element(CellStyle).Text("通貨ペア");
                    header.Cell().Element(CellStyle).Text("売");
                    header.Cell().Element(CellStyle).Text("買");
                    header.Cell().Element(CellStyle).Text("当初約定日時");
                    header.Cell().Element(CellStyle).Text("当初約定価格");
                    header.Cell().Element(CellStyle).Text("最新ロール値");
                    header.Cell().Element(CellStyle).Text("評価レート※");
                    header.Cell().Element(CellStyle).Text("評価損益");
                    header.Cell().Element(CellStyle).Text("累積スワップポイント");
                    header.Cell().Element(CellStyle).Text("累積ロール損益");
                });
                double total売 = 0;
                double total買 = 0;
                double total評価損益 = 0;
                double total累積SwapPoint = 0;
                double total累積ロール損益 = 0;

                foreach (var data in datas)
                {
                    table.Cell().Element(CellStyle).Text(data.通貨ペア);
                    table.Cell().Element(CellStyle).Text(data.売);
                    table.Cell().Element(CellStyle).Text(data.買);
                    table.Cell().Element(CellStyle).Text(data.当初約定日時.ToString("yyyy/MM/dd hh:mm:ss"));
                    table.Cell().Element(CellStyle).Text(data.当初約定価格.ToString());
                    table.Cell().Element(CellStyle).Text(data.最新ロール値.ToString());
                    table.Cell().Element(CellStyle).Text(data.評価レート.ToString());
                    table.Cell().Element(CellStyle).Text(data.評価損益.ToString());
                    table.Cell().Element(CellStyle).Text(data.累積SwapPoint.ToString());
                    table.Cell().Element(CellStyle).Text(data.累積ロール損益.ToString());
                    if (string.Equals(data.売, "1"))
                    {
                        total売++;
                    }
                    if (string.Equals(data.買, "1"))
                    {
                        total買++;
                    }
                    total評価損益 += data.評価損益;
                    total累積SwapPoint += data.累積SwapPoint;
                    total累積ロール損益 += data.累積ロール損益;
                }

                //合計
                table.Cell().Element(CellStyle).Text("合計");
                table.Cell().Element(CellStyle).Text(total売.ToString());
                table.Cell().Element(CellStyle).Text(total買.ToString());
                table.Cell().Element(CellStyle).Text("");
                table.Cell().Element(CellStyle).Text("");
                table.Cell().Element(CellStyle).Text("");
                table.Cell().Element(CellStyle).Text("");
                table.Cell().Element(CellStyle).Text(total評価損益.ToString());
                table.Cell().Element(CellStyle).Text(total累積SwapPoint.ToString());
                table.Cell().Element(CellStyle).Text(total累積ロール損益.ToString());
            });
        }
        /// <summary>
        /// お預り額
        /// </summary>
        /// <param name="container"></param>
        void DepositAmountDetails(IContainer container)
        {
            IContainer DefaultCellStyle(IContainer container, string backgroundColor)
            {
                return container
                    .Border((float)0.2)
                    .BorderColor(Colors.Black)
                    .Background(backgroundColor)
                    //.PaddingVertical(5)
                    //.PaddingHorizontal(10)
                    .AlignCenter()
                    .AlignMiddle()
                    .DefaultTextStyle(x => x.FontSize(defaultFontSize))
                    ;
            }

            container.Table(table =>
            {
                IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White);
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(80);//預託証拠金&現金(円)

                    columns.ConstantColumn(100);//必要証拠金(円)
                    columns.ConstantColumn(100);//取引可能額(円)
                });

                table.Header(header =>
                {
                    // please be sure to call the 'header' handler!

                    header.Cell().Element(CellStyle).Text("預託証拠金");

                    header.Cell().RowSpan(2).Element(CellStyle).Text("必要証拠金(円)");
                    header.Cell().RowSpan(2).Element(CellStyle).Text("取引可能額(円)");
                    header.Cell().Column(1).Row(2).Element(CellStyle).Text("現金(円)");

                });

                table.Cell().Element(CellStyle).Text("2,881,330");
                table.Cell().Element(CellStyle).Text("1,181,325");
                table.Cell().Element(CellStyle).Text("1,700,005");
            });

        }

        void DepositWithdrawalDetails(IContainer container)
        {
            IContainer DefaultCellStyle(IContainer container, string backgroundColor)
            {
                return container
                    .Border((float)0.2)
                    .BorderColor(Colors.Black)
                    .Background(backgroundColor)
                    //.PaddingVertical(5)
                    //.PaddingHorizontal(10)
                    .AlignCenter()
                    .AlignMiddle()
                    .DefaultTextStyle(x => x.FontSize(defaultFontSize))
                    ;
            }
            container.Table(table =>
            {
                IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White);
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(100);//日付

                    columns.ConstantColumn(50);//受領区分
                    columns.ConstantColumn(80);//取引市場 市場開設
                    columns.ConstantColumn(80);//証拠金(円)
                    columns.ConstantColumn(300);//備考
                });

                table.Header(header =>
                {
                    // please be sure to call the 'header' handler!

                    header.Cell().Element(CellStyle).Text("日付");

                    header.Cell().Element(CellStyle).Text("受領区分");
                    header.Cell().Element(CellStyle).Text("取引市場 市場開設");
                    header.Cell().Element(CellStyle).Text("証拠金(円)");
                    header.Cell().Element(CellStyle).Text("備考");
                    // you can extend existing styles by creating additional methods
                });

                table.Cell().ColumnSpan(5).Element(CellStyle).Text("お取引はございません");



                //table.Cell().AlignLeft().Text($"対象期間:{DateTime.Now.AddDays(-1).ToString("yyyy年 M月 d日 H:00:00")}～{DateTime.Now.ToString("yyyy年 M月 d日 H:00:00")}").FontSize(defaultFontSize);
                //table.Cell().AlignLeft().Text($"営業所の名称:本店　　取引の種類:店頭外国為替証拠金取引　　自己または委託の別：自己").FontSize(defaultFontSize);
                //table.Cell().AlignLeft().Text($"証拠金の種類：外国為替証拠金取引証拠金／現金　証拠金を預託すべき相手方：住信SBIネット銀行株式会社 分別管理上の預託先：三井住友信託銀行株式会社").FontSize(defaultFontSize);
            });
        }
        void CreateTransactionDetails(IContainer container)
        {

            var datas = new List<ReportData>
            {
                new ReportData{ 通貨ペア="米ﾄﾞﾙ/円",
                    新規区分="ロール",新規約定日時=DateTime.Now,新規売="1",新規買="",新規約定価格=161.668,
                    決済区分="ロール",決済約定日時=DateTime.Now,決済売="",決済買="1",決済約定価格=161.218,決済換算レート="",決済受渡日=DateTime.Now,
                    SwapPoint=-241,差損益金=4500,手数料=0,小計=4259
                },
                new ReportData{ 通貨ペア="ﾕｰﾛ/円",
                    新規区分="ロール",新規約定日時=DateTime.Now,新規売="1",新規買="",新規約定価格=161.668,
                    決済区分="ロール",決済約定日時=DateTime.Now,決済売="",決済買="1",決済約定価格=161.218,決済換算レート="",決済受渡日=DateTime.Now,
                    SwapPoint=-241,差損益金=4500,手数料=0,小計=4259
                },
                new ReportData{ 通貨ペア="豪ﾄﾞﾙ/米ﾄﾞﾙ",
                    新規区分="ロール",新規約定日時=DateTime.Now,新規売="1",新規買="",新規約定価格=161.668,
                    決済区分="ロール",決済約定日時=DateTime.Now,決済売="",決済買="1",決済約定価格=161.218,決済換算レート="",決済受渡日=DateTime.Now,
                    SwapPoint=-241,差損益金=4500,手数料=0,小計=4259
                },
                new ReportData{ 通貨ペア="英ﾎﾟﾝﾄﾞ/円",
                    新規区分="ロール",新規約定日時=DateTime.Now,新規売="1",新規買="",新規約定価格=161.668,
                    決済区分="ロール",決済約定日時=DateTime.Now,決済売="",決済買="1",決済約定価格=161.218,決済換算レート="",決済受渡日=DateTime.Now,
                    SwapPoint=-241,差損益金=4500,手数料=0,小計=4259
                },
                new ReportData{ 通貨ペア="米ﾄﾞﾙ/円",
                    新規区分="ロール",新規約定日時=DateTime.Now,新規売="1",新規買="",新規約定価格=161.668,
                    決済区分="ロール",決済約定日時=DateTime.Now,決済売="",決済買="1",決済約定価格=161.218,決済換算レート="",決済受渡日=DateTime.Now,
                    SwapPoint=-241,差損益金=4500,手数料=0,小計=4259
                },
                new ReportData{ 通貨ペア="米ﾄﾞﾙ/円",
                    新規区分="ロール",新規約定日時=DateTime.Now,新規売="1",新規買="",新規約定価格=161.668,
                    決済区分="ロール",決済約定日時=DateTime.Now,決済売="",決済買="1",決済約定価格=161.218,決済換算レート="",決済受渡日=DateTime.Now,
                    SwapPoint=-241,差損益金=4500,手数料=0,小計=4259
                },
                new ReportData{ 通貨ペア="米ﾄﾞﾙ/円",
                    新規区分="ロール",新規約定日時=DateTime.Now,新規売="1",新規買="",新規約定価格=161.668,
                    決済区分="ロール",決済約定日時=DateTime.Now,決済売="",決済買="1",決済約定価格=161.218,決済換算レート="",決済受渡日=DateTime.Now,
                    SwapPoint=-241,差損益金=4500,手数料=0,小計=4259
                },
                new ReportData{ 通貨ペア="米ﾄﾞﾙ/円",
                    新規区分="ロール",新規約定日時=DateTime.Now,新規売="1",新規買="",新規約定価格=161.668,
                    決済区分="ロール",決済約定日時=DateTime.Now,決済売="",決済買="1",決済約定価格=161.218,決済換算レート="",決済受渡日=DateTime.Now,
                    SwapPoint=-241,差損益金=4500,手数料=0,小計=4259
                },
                new ReportData{ 通貨ペア="米ﾄﾞﾙ/円",
                    新規区分="ロール",新規約定日時=DateTime.Now,新規売="1",新規買="",新規約定価格=161.668,
                    決済区分="ロール",決済約定日時=DateTime.Now,決済売="",決済買="1",決済約定価格=161.218,決済換算レート="",決済受渡日=DateTime.Now,
                    SwapPoint=-241,差損益金=4500,手数料=0,小計=4259
                },
                new ReportData{ 通貨ペア="米ﾄﾞﾙ/円",
                    新規区分="ロール",新規約定日時=DateTime.Now,新規売="1",新規買="",新規約定価格=161.668,
                    決済区分="ロール",決済約定日時=DateTime.Now,決済売="",決済買="1",決済約定価格=161.218,決済換算レート="",決済受渡日=DateTime.Now,
                    SwapPoint=-241,差損益金=4500,手数料=0,小計=4259
                },
                new ReportData{ 通貨ペア="米ﾄﾞﾙ/円",
                    新規区分="ロール",新規約定日時=DateTime.Now,新規売="1",新規買="",新規約定価格=161.668,
                    決済区分="ロール",決済約定日時=DateTime.Now,決済売="",決済買="1",決済約定価格=161.218,決済換算レート="",決済受渡日=DateTime.Now,
                    SwapPoint=-241,差損益金=4500,手数料=0,小計=4259
                },
                new ReportData{ 通貨ペア="米ﾄﾞﾙ/円",
                    新規区分="ロール",新規約定日時=DateTime.Now,新規売="1",新規買="",新規約定価格=161.668,
                    決済区分="ロール",決済約定日時=DateTime.Now,決済売="",決済買="1",決済約定価格=161.218,決済換算レート="",決済受渡日=DateTime.Now,
                    SwapPoint=-241,差損益金=4500,手数料=0,小計=4259
                },
                new ReportData{ 通貨ペア="米ﾄﾞﾙ/円",
                    新規区分="ロール",新規約定日時=DateTime.Now,新規売="1",新規買="",新規約定価格=161.668,
                    決済区分="ロール",決済約定日時=DateTime.Now,決済売="",決済買="1",決済約定価格=161.218,決済換算レート="",決済受渡日=DateTime.Now,
                    SwapPoint=-241,差損益金=4500,手数料=0,小計=4259
                },
                new ReportData{ 通貨ペア="米ﾄﾞﾙ/円",
                    新規区分="ロール",新規約定日時=DateTime.Now,新規売="1",新規買="",新規約定価格=161.668,
                    決済区分="ロール",決済約定日時=DateTime.Now,決済売="",決済買="1",決済約定価格=161.218,決済換算レート="",決済受渡日=DateTime.Now,
                    SwapPoint=-241,差損益金=4500,手数料=0,小計=4259
                },
            };

            IContainer DefaultCellStyle(IContainer container, string backgroundColor)
            {
                return container
                    .Border((float)0.2)
                    .BorderColor(Colors.Black)
                    .Background(backgroundColor)
                    //.PaddingVertical(5)
                    //.PaddingHorizontal(10)
                    .AlignCenter()
                    .AlignMiddle()
                    .DefaultTextStyle(x => x.FontSize(defaultFontSize))
                    ;
            }
            container.Table(table =>
            {
                IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White);
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(50);//通貨ペア

                    //新規(区分,約定日時,売,買,約定価格)
                    columns.ConstantColumn(40);//区分
                    columns.ConstantColumn(50);//約定日時
                    columns.ConstantColumn(20);//売
                    columns.ConstantColumn(20);//買
                    columns.ConstantColumn(30);//約定価格


                    //決済(区分,約定日時,売,買,約定価格,換算レート,受渡日)
                    columns.ConstantColumn(20);//区分
                    columns.ConstantColumn(50);//約定日時
                    columns.ConstantColumn(20);//売
                    columns.ConstantColumn(20);//買
                    columns.ConstantColumn(30);//約定価格
                    columns.ConstantColumn(30);//換算レート
                    columns.ConstantColumn(40);//受渡日

                    columns.ConstantColumn(50);//スワップ ポイント
                    columns.ConstantColumn(50);//差損益金
                    columns.ConstantColumn(50);//手数料
                    columns.ConstantColumn(50);//小計

                });

                table.Header(header =>
                {
                    // please be sure to call the 'header' handler!

                    header.Cell().RowSpan(2).Element(CellStyle).Text("通貨ペア");

                    header.Cell().ColumnSpan(5).Element(CellStyle).Text("新規");
                    header.Cell().ColumnSpan(7).Element(CellStyle).Text("決済");

                    header.Cell().RowSpan(2).Element(CellStyle).Text("スワップ ポイント");
                    header.Cell().RowSpan(2).Element(CellStyle).Text("差損益金");
                    header.Cell().RowSpan(2).Element(CellStyle).Text("手数料");
                    header.Cell().RowSpan(2).Element(CellStyle).Text("小計");

                    header.Cell().Column(2).Row(2).Element(CellStyle).Text("区分");
                    header.Cell().Column(3).Row(2).Element(CellStyle).Text("約定日時");
                    header.Cell().Column(4).Row(2).Element(CellStyle).Text("売");
                    header.Cell().Column(5).Row(2).Element(CellStyle).Text("買");
                    header.Cell().Column(6).Row(2).Element(CellStyle).Text("約定価格");

                    header.Cell().Column(7).Row(2).Element(CellStyle).Text("区分");
                    header.Cell().Column(8).Row(2).Element(CellStyle).Text("約定日時");
                    header.Cell().Column(9).Row(2).Element(CellStyle).Text("売");
                    header.Cell().Column(10).Row(2).Element(CellStyle).Text("買");
                    header.Cell().Column(11).Row(2).Element(CellStyle).Text("約定価格");
                    header.Cell().Column(12).Row(2).Element(CellStyle).Text("換算レート");
                    header.Cell().Column(13).Row(2).Element(CellStyle).Text("受渡日");

                    // you can extend existing styles by creating additional methods
                });
                foreach (var data in datas)
                {

                    table.Cell().Element(CellStyle).Text(data.通貨ペア);

                    table.Cell().Element(CellStyle).Text(data.新規区分);
                    table.Cell().Element(CellStyle).Text(data.新規約定日時.ToString("yyyy/MM/dd hh:mm:ss"));
                    table.Cell().Element(CellStyle).Text(data.新規売);
                    table.Cell().Element(CellStyle).Text(data.新規買);
                    table.Cell().Element(CellStyle).Text(data.新規約定価格.ToString());


                    table.Cell().Element(CellStyle).Text(data.決済区分);
                    table.Cell().Element(CellStyle).Text(data.決済約定日時.ToString("yyyy/MM/dd hh:mm:ss"));
                    table.Cell().Element(CellStyle).Text(data.決済売);
                    table.Cell().Element(CellStyle).Text(data.決済買);
                    table.Cell().Element(CellStyle).Text(data.決済約定価格.ToString());
                    table.Cell().Element(CellStyle).Text(data.決済換算レート);
                    table.Cell().Element(CellStyle).Text(data.決済受渡日.ToString("yyyy/MM/dd"));

                    table.Cell().Element(CellStyle).Text(data.SwapPoint.ToString());
                    table.Cell().Element(CellStyle).Text(data.差損益金.ToString());
                    table.Cell().Element(CellStyle).Text(data.手数料.ToString());
                    table.Cell().Element(CellStyle).Text(data.小計.ToString("0,000"));
                }



                //table.Cell().AlignLeft().Text($"対象期間:{DateTime.Now.AddDays(-1).ToString("yyyy年 M月 d日 H:00:00")}～{DateTime.Now.ToString("yyyy年 M月 d日 H:00:00")}").FontSize(defaultFontSize);
                //table.Cell().AlignLeft().Text($"営業所の名称:本店　　取引の種類:店頭外国為替証拠金取引　　自己または委託の別：自己").FontSize(defaultFontSize);
                //table.Cell().AlignLeft().Text($"証拠金の種類：外国為替証拠金取引証拠金／現金　証拠金を預託すべき相手方：住信SBIネット銀行株式会社 分別管理上の預託先：三井住友信託銀行株式会社").FontSize(defaultFontSize);
            });
        }

        void CreateKouzaInfo(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();
                });

                table.Cell().AlignLeft().Text($"対象期間:{DateTime.Now.AddDays(-1).ToString("yyyy年 M月 d日 H:00:00")}～{DateTime.Now.ToString("yyyy年 M月 d日 H:00:00")}").FontSize(defaultFontSize);
                table.Cell().AlignLeft().Text($"営業所の名称:本店　　取引の種類:証拠金取引　　自己または委託の別：自己").FontSize(defaultFontSize);
                table.Cell().AlignLeft().Text($"証拠金の種類：証拠金取引　相手方：テスト銀行株式会社 分別管理上の預託先：テストAAA銀行株式会社").FontSize(defaultFontSize);
            });

        }
        void CreateTitle(IContainer container)
        {
            //var headerStyle = TextStyle.Default.SemiBold();

            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(3);
                    columns.RelativeColumn((float)6.7);
                    columns.RelativeColumn((float)1.3);
                });


                table.Cell().ColumnSpan(3).AlignCenter().Text("取引報告書兼証拠金受領書兼取引残高報告書").FontSize(12).FontFamily("HeiseiMin-W3").Bold();

                table.Cell().AlignLeft().Text($"{"テスト用"}　様").FontSize(defaultFontSize);
                table.Cell().AlignLeft().Text($"いつもテスト銀行株式会社をご利用いただきまして、誠にありがとうございます。").FontSize(defaultFontSize);
                table.Cell().AlignLeft().Text("東京都").FontSize(defaultFontSize);

                table.Cell().AlignLeft().Text("").FontSize(defaultFontSize);
                table.Cell().AlignLeft().Text($"下記のとおり、お取引口座残高並びにお取引明細をご報告申しあげます。").FontSize(defaultFontSize);
                table.Cell().AlignLeft().Text("東京都").FontSize(defaultFontSize);

                table.Cell().AlignLeft().Text("").FontSize(defaultFontSize);
                table.Cell().AlignLeft().Text($"内容につきご確認いただき、ご不明な点がございましたら、カスタマーセンターまでご連絡ください。").FontSize(7);
                table.Cell().AlignLeft().Text("テスト銀行株式会社").FontSize(defaultFontSize);

                table.Cell().AlignLeft().Text("").FontSize(defaultFontSize);
                table.Cell().AlignLeft().Text($"").FontSize(defaultFontSize);
                table.Cell().AlignLeft().Text("関東財務局").FontSize(defaultFontSize);

                table.Cell().AlignLeft().Text("").FontSize(defaultFontSize);
                table.Cell().AlignLeft().Text($"口座番号:{123}").FontSize(defaultFontSize);
                table.Cell().AlignLeft().Text("カスタマーセンター").FontSize(defaultFontSize);

                table.Cell().AlignLeft().Text("").FontSize(defaultFontSize);
                table.Cell().AlignLeft().Text($"").FontSize(defaultFontSize);
                table.Cell().AlignLeft().Text("000-000-001 (通話料無料)").FontSize(defaultFontSize);

                table.Cell().AlignLeft().Text("").FontSize(defaultFontSize);
                table.Cell().AlignLeft().Text($"").FontSize(defaultFontSize);
                table.Cell().AlignLeft().Text("000-000-001 (通話料有料)").FontSize(defaultFontSize);




                //table.Cell().AlignLeft().Text($"お取引明細").FontSize(defaultFontSize);
            });


        }
        private void BuildHeaderInfo(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().AlignRight().Column(column =>
                {
                    column.Item().AlignRight().Text(text =>
                   {
                       text.Span($"No.         ").FontSize(7).FontFamily("HeiseiMin-W3");
                       text.CurrentPageNumber().FontSize(7).FontFamily("HeiseiMin-W3");
                   });

                    column.Item().AlignRight().Text(text =>
                    {
                        text.Span($"発行日:{DateTime.Now.ToString("yyyy/MM/dd")}").FontSize(7).FontFamily("HeiseiMin-W3");
                    });
                });


            });

        }
    }
}
