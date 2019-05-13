
$(document).ready(function () {
   

    chartContainer1();
    chartContainer2();
    chartContainer3();
    chartContainer4();
    chartContainer5();
    chartContainer6();
    chartContainer7();
    chartContainer8();
    chartContainer9();
    chartContainer10();
    chartContainer11();
    chartContainer12();
    chartContainer13();
    chartContainer14();
    chartContainer15();
    chartContainer16();
    chartContainer17();
    chartContainer18();
    chartContainer19();
    chartContainer20();
    chartContainer21();
    chartContainer22();
    chartContainer23();
    chartContainer24();
    chartContainer25();
    chartContainer26();
    chartContainer27();
    chartContainer28();
});

//Phần Xuất Ra Biểu Đồ Hình Cột
//Phần Xuất Ra Biểu Đồ Dòng 1
function chartContainer1() {
    
    //Phần lấy ra Tiêu Đề
    var title = document.getElementById("ExcelView").rows[1].cells[3].innerText;
    var title1 = "Biểu Đồ Tổng Hợp SL Đi Phát Của " + title;

    //Lấy Thông Tin SL BƯU GỬI ĐẾN
    //Lấy tiêu đề của biểu đồ
    try {
        var a = document.getElementById("ExcelView").rows[0].cells[4].innerText;
    }
    catch (e) {
        var a = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var a1 = parseInt(document.getElementById("ExcelView").rows[1].cells[4].innerText);
    }
    catch (e) {
        var a1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT THÀNH CÔNG
    //Lấy tiêu đề của biểu đồ
    try {
        var b = document.getElementById("ExcelView").rows[0].cells[5].innerText;
    }
    catch (e) {
        var b = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var b1 = parseInt(document.getElementById("ExcelView").rows[1].cells[5].innerText);
    }
    catch (e) {
        var b1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT CHƯA CÓ THÔNG TIN
    //Lấy tiêu đề của biểu đồ
    try {
        var c = document.getElementById("ExcelView").rows[0].cells[6].innerText;
    }
    catch (e) {
        var c = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var c1 = parseInt(document.getElementById("ExcelView").rows[1].cells[6].innerText);
    }
    catch (e) {
        var c1 = "Null";
    }

    //Lấy Thông Tin SL PTC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var d = document.getElementById("ExcelView").rows[0].cells[7].innerText;
    }
    catch (e) {
        var d = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var d1 = parseInt(document.getElementById("ExcelView").rows[1].cells[7].innerText);

    }
    catch (e) {
        var d1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var e = document.getElementById("ExcelView").rows[0].cells[8].innerText;
    }
    catch (e) {
        var e = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var e1 = parseInt(document.getElementById("ExcelView").rows[1].cells[8].innerText);

    }
    catch (e) {
        var e1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG XÁC ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var f = document.getElementById("ExcelView").rows[0].cells[11].innerText;
    }
    catch (e) {
        var f = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var f1 = parseInt(document.getElementById("ExcelView").rows[1].cells[11].innerText);

    }
    catch (e) {
        var f1 = "Null";

    }


    chart1 = new CanvasJS.Chart("chartContainer1",
    {
        animationEnabled: true,  
        title: {
            text: title1,
            fontFamily: "tahoma",
        },

        axisY: {
            title: title,
            //titleMaxWidth: 150 //**change the value of titleMaxWidth
        },

        data: [
        {
            type: "column",
            indexLabel: "{y}",
            dataPoints: [
            { label: a, y: a1 },
            { label: b, y: b1 },
            { label: c, y: c1 },
            { label: d, y: d1 },
            { label: e, y: e1 },
            { label: f, y: f1 },
            ]
        }
        ]
    });

    chart1.render();
}



function chartContainer2() {
    //Phần Xuất Ra Biểu Đồ Hình Tròn

    //Phần Lấy Ra Tiêu Đề Biểu Đồ Hình Tròn
    var titleTron = document.getElementById("ExcelView").rows[1].cells[3].innerText;
    var titleTron1 = "Tỉ Lệ PTC Của " + titleTron;

    //Lấy Thông Tin Tỉ Lệ TC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeTRUE = document.getElementById("ExcelView").rows[0].cells[9].innerText;
    }
    catch (e) {
        var tieudeTRUE = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var giatriTRUE = parseInt(document.getElementById("ExcelView").rows[1].cells[9].innerText);
    }
    catch (e) {
        var giatriTRUE = "Null";
    }

    //Lấy Thông Tin Tỉ Lệ TC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeFALSE = document.getElementById("ExcelView").rows[0].cells[10].innerText;
    }
    catch (e) {
        var tieudeFALSE = "Null";
    }

    //Lấy gía trị của biểu đồ
    try {
        var giatriFALSE = parseInt(document.getElementById("ExcelView").rows[1].cells[10].innerText);
    }
    catch (e) {
        var giatriFALSE = "Null";
    }


     chart2 = new CanvasJS.Chart("chartContainer2", {
        exportEnabled: true,
        animationEnabled: true,
        title: {
            text: titleTron1,
            fontFamily: "tahoma",
        },
        legend: {
            cursor: "pointer",
            itemclick: explodePie
        },
        data: [{
            type: "pie",
            showInLegend: true,
            toolTipContent: "{name}: <strong>{y}%</strong>",
            indexLabel: "{name} - {y}%",
            dataPoints: [
                { y: giatriTRUE, name: tieudeTRUE, exploded: true },
                { y: giatriFALSE, name: tieudeFALSE },


            ]
        }]
    });
    chart2.render();
}



//Phần Xuất Ra Biểu Đồ Dòng 2
function chartContainer3() {

    //Phần lấy ra Tiêu Đề
    var title = document.getElementById("ExcelView").rows[2].cells[3].innerText;
    var title1 = "Biểu Đồ Tổng Hợp SL Đi Phát Của " + title;

    //Lấy Thông Tin SL BƯU GỬI ĐẾN
    //Lấy tiêu đề của biểu đồ
    try {
        var a = document.getElementById("ExcelView").rows[0].cells[4].innerText;
    }
    catch (e) {
        var a = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var a1 = parseInt(document.getElementById("ExcelView").rows[2].cells[4].innerText);
    }
    catch (e) {
        var a1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT THÀNH CÔNG
    //Lấy tiêu đề của biểu đồ
    try {
        var b = document.getElementById("ExcelView").rows[0].cells[5].innerText;
    }
    catch (e) {
        var b = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var b1 = parseInt(document.getElementById("ExcelView").rows[2].cells[5].innerText);
    }
    catch (e) {
        var b1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT CHƯA CÓ THÔNG TIN
    //Lấy tiêu đề của biểu đồ
    try {
        var c = document.getElementById("ExcelView").rows[0].cells[6].innerText;
    }
    catch (e) {
        var c = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var c1 = parseInt(document.getElementById("ExcelView").rows[2].cells[6].innerText);
    }
    catch (e) {
        var c1 = "Null";
    }

    //Lấy Thông Tin SL PTC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var d = document.getElementById("ExcelView").rows[0].cells[7].innerText;
    }
    catch (e) {
        var d = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var d1 = parseInt(document.getElementById("ExcelView").rows[2].cells[7].innerText);

    }
    catch (e) {
        var d1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var e = document.getElementById("ExcelView").rows[0].cells[8].innerText;
    }
    catch (e) {
        var e = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var e1 = parseInt(document.getElementById("ExcelView").rows[2].cells[8].innerText);

    }
    catch (e) {
        var e1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG XÁC ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var f = document.getElementById("ExcelView").rows[0].cells[11].innerText;
    }
    catch (e) {
        var f = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var f1 = parseInt(document.getElementById("ExcelView").rows[2].cells[11].innerText);

    }
    catch (e) {
        var f1 = "Null";

    }


     chart3 = new CanvasJS.Chart("chartContainer3",
    {
        animationEnabled: true,
        title: {
            text: title1,
            fontFamily: "tahoma",
        },

        axisY: {
            title: title,
            //titleMaxWidth: 150 //**change the value of titleMaxWidth
        },

        data: [
        {
            type: "column",
            indexLabel: "{y}",
            dataPoints: [
            { label: a, y: a1 },
            { label: b, y: b1 },
            { label: c, y: c1 },
            { label: d, y: d1 },
            { label: e, y: e1 },
            { label: f, y: f1 },
            ]
        }
        ]
    });

    chart3.render();
}



function chartContainer4() {
    //Phần Xuất Ra Biểu Đồ Hình Tròn

    //Phần Lấy Ra Tiêu Đề Biểu Đồ Hình Tròn
    var titleTron = document.getElementById("ExcelView").rows[2].cells[3].innerText;
    var titleTron1 = "Tỉ Lệ PTC Của " + titleTron;

    //Lấy Thông Tin Tỉ Lệ TC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeTRUE = document.getElementById("ExcelView").rows[0].cells[9].innerText;
    }
    catch (e) {
        var tieudeTRUE = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var giatriTRUE = parseInt(document.getElementById("ExcelView").rows[2].cells[9].innerText);
    }
    catch (e) {
        var giatriTRUE = "Null";
    }

    //Lấy Thông Tin Tỉ Lệ TC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeFALSE = document.getElementById("ExcelView").rows[0].cells[10].innerText;
    }
    catch (e) {
        var tieudeFALSE = "Null";
    }

    //Lấy gía trị của biểu đồ
    try {
        var giatriFALSE = parseInt(document.getElementById("ExcelView").rows[2].cells[10].innerText);
    }
    catch (e) {
        var giatriFALSE = "Null";
    }


     chart4 = new CanvasJS.Chart("chartContainer4", {
        exportEnabled: true,
        animationEnabled: true,
        title: {
            text: titleTron1,
            fontFamily: "tahoma",
        },
        legend: {
            cursor: "pointer",
            itemclick: explodePie
        },
        data: [{
            type: "pie",
            showInLegend: true,
            toolTipContent: "{name}: <strong>{y}%</strong>",
            indexLabel: "{name} - {y}%",
            dataPoints: [
                { y: giatriTRUE, name: tieudeTRUE, exploded: true },
                { y: giatriFALSE, name: tieudeFALSE },


            ]
        }]
    });
    chart4.render();
}

//Phần Xuất Ra Biểu Đồ Dòng 3
function chartContainer5() {

    //Phần lấy ra Tiêu Đề
    var title = document.getElementById("ExcelView").rows[3].cells[3].innerText;
    var title1 = "Biểu Đồ Tổng Hợp SL Đi Phát Của " + title;

    //Lấy Thông Tin SL BƯU GỬI ĐẾN
    //Lấy tiêu đề của biểu đồ
    try {
        var a = document.getElementById("ExcelView").rows[0].cells[4].innerText;
    }
    catch (e) {
        var a = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var a1 = parseInt(document.getElementById("ExcelView").rows[3].cells[4].innerText);
    }
    catch (e) {
        var a1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT THÀNH CÔNG
    //Lấy tiêu đề của biểu đồ
    try {
        var b = document.getElementById("ExcelView").rows[0].cells[5].innerText;
    }
    catch (e) {
        var b = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var b1 = parseInt(document.getElementById("ExcelView").rows[3].cells[5].innerText);
    }
    catch (e) {
        var b1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT CHƯA CÓ THÔNG TIN
    //Lấy tiêu đề của biểu đồ
    try {
        var c = document.getElementById("ExcelView").rows[0].cells[6].innerText;
    }
    catch (e) {
        var c = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var c1 = parseInt(document.getElementById("ExcelView").rows[3].cells[6].innerText);
    }
    catch (e) {
        var c1 = "Null";
    }

    //Lấy Thông Tin SL PTC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var d = document.getElementById("ExcelView").rows[0].cells[7].innerText;
    }
    catch (e) {
        var d = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var d1 = parseInt(document.getElementById("ExcelView").rows[3].cells[7].innerText);

    }
    catch (e) {
        var d1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var e = document.getElementById("ExcelView").rows[0].cells[8].innerText;
    }
    catch (e) {
        var e = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var e1 = parseInt(document.getElementById("ExcelView").rows[3].cells[8].innerText);

    }
    catch (e) {
        var e1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG XÁC ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var f = document.getElementById("ExcelView").rows[0].cells[11].innerText;
    }
    catch (e) {
        var f = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var f1 = parseInt(document.getElementById("ExcelView").rows[3].cells[11].innerText);

    }
    catch (e) {
        var f1 = "Null";

    }


     chart5 = new CanvasJS.Chart("chartContainer5",
    {
        animationEnabled: true,
        title: {
            text: title1,
            fontFamily: "tahoma",
        },

        axisY: {
            title: title,
            //titleMaxWidth: 150 //**change the value of titleMaxWidth
        },

        data: [
        {
            type: "column",
            indexLabel: "{y}",
            dataPoints: [
            { label: a, y: a1 },
            { label: b, y: b1 },
            { label: c, y: c1 },
            { label: d, y: d1 },
            { label: e, y: e1 },
            { label: f, y: f1 },
            ]
        }
        ]
    });

    chart5.render();
}



function chartContainer6() {
    //Phần Xuất Ra Biểu Đồ Hình Tròn

    //Phần Lấy Ra Tiêu Đề Biểu Đồ Hình Tròn
    var titleTron = document.getElementById("ExcelView").rows[3].cells[3].innerText;
    var titleTron1 = "Tỉ Lệ PTC Của " + titleTron;

    //Lấy Thông Tin Tỉ Lệ TC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeTRUE = document.getElementById("ExcelView").rows[0].cells[9].innerText;
    }
    catch (e) {
        var tieudeTRUE = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var giatriTRUE = parseInt(document.getElementById("ExcelView").rows[3].cells[9].innerText);
    }
    catch (e) {
        var giatriTRUE = "Null";
    }

    //Lấy Thông Tin Tỉ Lệ TC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeFALSE = document.getElementById("ExcelView").rows[0].cells[10].innerText;
    }
    catch (e) {
        var tieudeFALSE = "Null";
    }

    //Lấy gía trị của biểu đồ
    try {
        var giatriFALSE = parseInt(document.getElementById("ExcelView").rows[3].cells[10].innerText);
    }
    catch (e) {
        var giatriFALSE = "Null";
    }


     chart6 = new CanvasJS.Chart("chartContainer6", {
        exportEnabled: true,
        animationEnabled: true,
        title: {
            text: titleTron1,
            fontFamily: "tahoma",
        },
        legend: {
            cursor: "pointer",
            itemclick: explodePie
        },
        data: [{
            type: "pie",
            showInLegend: true,
            toolTipContent: "{name}: <strong>{y}%</strong>",
            indexLabel: "{name} - {y}%",
            dataPoints: [
                { y: giatriTRUE, name: tieudeTRUE, exploded: true },
                { y: giatriFALSE, name: tieudeFALSE },


            ]
        }]
    });
    chart6.render();
}

//Phần Xuất Ra Biểu Đồ Dòng 4
function chartContainer7() {

    //Phần lấy ra Tiêu Đề
    var title = document.getElementById("ExcelView").rows[4].cells[3].innerText;
    var title1 = "Biểu Đồ Tổng Hợp SL Đi Phát Của " + title;

    //Lấy Thông Tin SL BƯU GỬI ĐẾN
    //Lấy tiêu đề của biểu đồ
    try {
        var a = document.getElementById("ExcelView").rows[0].cells[4].innerText;
    }
    catch (e) {
        var a = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var a1 = parseInt(document.getElementById("ExcelView").rows[4].cells[4].innerText);
    }
    catch (e) {
        var a1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT THÀNH CÔNG
    //Lấy tiêu đề của biểu đồ
    try {
        var b = document.getElementById("ExcelView").rows[0].cells[5].innerText;
    }
    catch (e) {
        var b = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var b1 = parseInt(document.getElementById("ExcelView").rows[4].cells[5].innerText);
    }
    catch (e) {
        var b1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT CHƯA CÓ THÔNG TIN
    //Lấy tiêu đề của biểu đồ
    try {
        var c = document.getElementById("ExcelView").rows[0].cells[6].innerText;
    }
    catch (e) {
        var c = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var c1 = parseInt(document.getElementById("ExcelView").rows[4].cells[6].innerText);
    }
    catch (e) {
        var c1 = "Null";
    }

    //Lấy Thông Tin SL PTC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var d = document.getElementById("ExcelView").rows[0].cells[7].innerText;
    }
    catch (e) {
        var d = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var d1 = parseInt(document.getElementById("ExcelView").rows[4].cells[7].innerText);

    }
    catch (e) {
        var d1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var e = document.getElementById("ExcelView").rows[0].cells[8].innerText;
    }
    catch (e) {
        var e = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var e1 = parseInt(document.getElementById("ExcelView").rows[4].cells[8].innerText);

    }
    catch (e) {
        var e1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG XÁC ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var f = document.getElementById("ExcelView").rows[0].cells[11].innerText;
    }
    catch (e) {
        var f = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var f1 = parseInt(document.getElementById("ExcelView").rows[4].cells[11].innerText);

    }
    catch (e) {
        var f1 = "Null";

    }


     chart7 = new CanvasJS.Chart("chartContainer7",
    {
        animationEnabled: true,
        title: {
            text: title1,
            fontFamily: "tahoma",
        },

        axisY: {
            title: title,
            //titleMaxWidth: 150 //**change the value of titleMaxWidth
        },

        data: [
        {
            type: "column",
            indexLabel: "{y}",
            dataPoints: [
            { label: a, y: a1 },
            { label: b, y: b1 },
            { label: c, y: c1 },
            { label: d, y: d1 },
            { label: e, y: e1 },
            { label: f, y: f1 },
            ]
        }
        ]
    });

    chart7.render();
}



function chartContainer8() {
    //Phần Xuất Ra Biểu Đồ Hình Tròn

    //Phần Lấy Ra Tiêu Đề Biểu Đồ Hình Tròn
    var titleTron = document.getElementById("ExcelView").rows[4].cells[3].innerText;
    var titleTron1 = "Tỉ Lệ PTC Của " + titleTron;

    //Lấy Thông Tin Tỉ Lệ TC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeTRUE = document.getElementById("ExcelView").rows[0].cells[9].innerText;
    }
    catch (e) {
        var tieudeTRUE = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var giatriTRUE = parseInt(document.getElementById("ExcelView").rows[4].cells[9].innerText);
    }
    catch (e) {
        var giatriTRUE = "Null";
    }

    //Lấy Thông Tin Tỉ Lệ TC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeFALSE = document.getElementById("ExcelView").rows[0].cells[10].innerText;
    }
    catch (e) {
        var tieudeFALSE = "Null";
    }

    //Lấy gía trị của biểu đồ
    try {
        var giatriFALSE = parseInt(document.getElementById("ExcelView").rows[4].cells[10].innerText);
    }
    catch (e) {
        var giatriFALSE = "Null";
    }


     chart8 = new CanvasJS.Chart("chartContainer8", {
        exportEnabled: true,
        animationEnabled: true,
        title: {
            text: titleTron1,
            fontFamily: "tahoma",
        },
        legend: {
            cursor: "pointer",
            itemclick: explodePie
        },
        data: [{
            type: "pie",
            showInLegend: true,
            toolTipContent: "{name}: <strong>{y}%</strong>",
            indexLabel: "{name} - {y}%",
            dataPoints: [
                { y: giatriTRUE, name: tieudeTRUE, exploded: true },
                { y: giatriFALSE, name: tieudeFALSE },


            ]
        }]
    });
    chart8.render();
}

//Phần Xuất Ra Biểu Đồ Dòng 5
function chartContainer9() {

    //Phần lấy ra Tiêu Đề
    var title = document.getElementById("ExcelView").rows[5].cells[3].innerText;
    var title1 = "Biểu Đồ Tổng Hợp SL Đi Phát Của " + title;

    //Lấy Thông Tin SL BƯU GỬI ĐẾN
    //Lấy tiêu đề của biểu đồ
    try {
        var a = document.getElementById("ExcelView").rows[0].cells[4].innerText;
    }
    catch (e) {
        var a = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var a1 = parseInt(document.getElementById("ExcelView").rows[5].cells[4].innerText);
    }
    catch (e) {
        var a1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT THÀNH CÔNG
    //Lấy tiêu đề của biểu đồ
    try {
        var b = document.getElementById("ExcelView").rows[0].cells[5].innerText;
    }
    catch (e) {
        var b = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var b1 = parseInt(document.getElementById("ExcelView").rows[5].cells[5].innerText);
    }
    catch (e) {
        var b1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT CHƯA CÓ THÔNG TIN
    //Lấy tiêu đề của biểu đồ
    try {
        var c = document.getElementById("ExcelView").rows[0].cells[6].innerText;
    }
    catch (e) {
        var c = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var c1 = parseInt(document.getElementById("ExcelView").rows[5].cells[6].innerText);
    }
    catch (e) {
        var c1 = "Null";
    }

    //Lấy Thông Tin SL PTC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var d = document.getElementById("ExcelView").rows[0].cells[7].innerText;
    }
    catch (e) {
        var d = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var d1 = parseInt(document.getElementById("ExcelView").rows[5].cells[7].innerText);

    }
    catch (e) {
        var d1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var e = document.getElementById("ExcelView").rows[0].cells[8].innerText;
    }
    catch (e) {
        var e = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var e1 = parseInt(document.getElementById("ExcelView").rows[5].cells[8].innerText);

    }
    catch (e) {
        var e1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG XÁC ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var f = document.getElementById("ExcelView").rows[0].cells[11].innerText;
    }
    catch (e) {
        var f = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var f1 = parseInt(document.getElementById("ExcelView").rows[5].cells[11].innerText);

    }
    catch (e) {
        var f1 = "Null";

    }


     chart9 = new CanvasJS.Chart("chartContainer9",
    {
        animationEnabled: true,
        title: {
            text: title1,
            fontFamily: "tahoma",
        },

        axisY: {
            title: title,
            //titleMaxWidth: 150 //**change the value of titleMaxWidth
        },

        data: [
        {
            type: "column",
            indexLabel: "{y}",
            dataPoints: [
            { label: a, y: a1 },
            { label: b, y: b1 },
            { label: c, y: c1 },
            { label: d, y: d1 },
            { label: e, y: e1 },
            { label: f, y: f1 },
            ]
        }
        ]
    });

    chart9.render();
}



function chartContainer10() {
    //Phần Xuất Ra Biểu Đồ Hình Tròn

    //Phần Lấy Ra Tiêu Đề Biểu Đồ Hình Tròn
    var titleTron = document.getElementById("ExcelView").rows[5].cells[3].innerText;
    var titleTron1 = "Tỉ Lệ PTC Của " + titleTron;

    //Lấy Thông Tin Tỉ Lệ TC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeTRUE = document.getElementById("ExcelView").rows[0].cells[9].innerText;
    }
    catch (e) {
        var tieudeTRUE = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var giatriTRUE = parseInt(document.getElementById("ExcelView").rows[5].cells[9].innerText);
    }
    catch (e) {
        var giatriTRUE = "Null";
    }

    //Lấy Thông Tin Tỉ Lệ TC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeFALSE = document.getElementById("ExcelView").rows[0].cells[10].innerText;
    }
    catch (e) {
        var tieudeFALSE = "Null";
    }

    //Lấy gía trị của biểu đồ
    try {
        var giatriFALSE = parseInt(document.getElementById("ExcelView").rows[5].cells[10].innerText);
    }
    catch (e) {
        var giatriFALSE = "Null";
    }


     chart10 = new CanvasJS.Chart("chartContainer10", {
        exportEnabled: true,
        animationEnabled: true,
        title: {
            text: titleTron1,
            fontFamily: "tahoma",
        },
        legend: {
            cursor: "pointer",
            itemclick: explodePie
        },
        data: [{
            type: "pie",
            showInLegend: true,
            toolTipContent: "{name}: <strong>{y}%</strong>",
            indexLabel: "{name} - {y}%",
            dataPoints: [
                { y: giatriTRUE, name: tieudeTRUE, exploded: true },
                { y: giatriFALSE, name: tieudeFALSE },


            ]
        }]
    });
    chart10.render();
}

//Phần Xuất Ra Biểu Đồ Dòng 6
function chartContainer11() {

    //Phần lấy ra Tiêu Đề
    var title = document.getElementById("ExcelView").rows[6].cells[3].innerText;
    var title1 = "Biểu Đồ Tổng Hợp SL Đi Phát Của " + title;

    //Lấy Thông Tin SL BƯU GỬI ĐẾN
    //Lấy tiêu đề của biểu đồ
    try {
        var a = document.getElementById("ExcelView").rows[0].cells[4].innerText;
    }
    catch (e) {
        var a = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var a1 = parseInt(document.getElementById("ExcelView").rows[6].cells[4].innerText);
    }
    catch (e) {
        var a1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT THÀNH CÔNG
    //Lấy tiêu đề của biểu đồ
    try {
        var b = document.getElementById("ExcelView").rows[0].cells[5].innerText;
    }
    catch (e) {
        var b = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var b1 = parseInt(document.getElementById("ExcelView").rows[6].cells[5].innerText);
    }
    catch (e) {
        var b1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT CHƯA CÓ THÔNG TIN
    //Lấy tiêu đề của biểu đồ
    try {
        var c = document.getElementById("ExcelView").rows[0].cells[6].innerText;
    }
    catch (e) {
        var c = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var c1 = parseInt(document.getElementById("ExcelView").rows[6].cells[6].innerText);
    }
    catch (e) {
        var c1 = "Null";
    }

    //Lấy Thông Tin SL PTC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var d = document.getElementById("ExcelView").rows[0].cells[7].innerText;
    }
    catch (e) {
        var d = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var d1 = parseInt(document.getElementById("ExcelView").rows[6].cells[7].innerText);

    }
    catch (e) {
        var d1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var e = document.getElementById("ExcelView").rows[0].cells[8].innerText;
    }
    catch (e) {
        var e = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var e1 = parseInt(document.getElementById("ExcelView").rows[6].cells[8].innerText);

    }
    catch (e) {
        var e1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG XÁC ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var f = document.getElementById("ExcelView").rows[0].cells[11].innerText;
    }
    catch (e) {
        var f = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var f1 = parseInt(document.getElementById("ExcelView").rows[6].cells[11].innerText);

    }
    catch (e) {
        var f1 = "Null";

    }


     chart11 = new CanvasJS.Chart("chartContainer11",
    {
        animationEnabled: true,
        title: {
            text: title1,
            fontFamily: "tahoma",
        },

        axisY: {
            title: title,
            //titleMaxWidth: 150 //**change the value of titleMaxWidth
        },

        data: [
        {
            type: "column",
            indexLabel: "{y}",
            dataPoints: [
            { label: a, y: a1 },
            { label: b, y: b1 },
            { label: c, y: c1 },
            { label: d, y: d1 },
            { label: e, y: e1 },
            { label: f, y: f1 },
            ]
        }
        ]
    });

    chart11.render();
}



function chartContainer12() {
    //Phần Xuất Ra Biểu Đồ Hình Tròn

    //Phần Lấy Ra Tiêu Đề Biểu Đồ Hình Tròn
    var titleTron = document.getElementById("ExcelView").rows[6].cells[3].innerText;
    var titleTron1 = "Tỉ Lệ PTC Của " + titleTron;

    //Lấy Thông Tin Tỉ Lệ TC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeTRUE = document.getElementById("ExcelView").rows[0].cells[9].innerText;
    }
    catch (e) {
        var tieudeTRUE = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var giatriTRUE = parseInt(document.getElementById("ExcelView").rows[6].cells[9].innerText);
    }
    catch (e) {
        var giatriTRUE = "Null";
    }

    //Lấy Thông Tin Tỉ Lệ TC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeFALSE = document.getElementById("ExcelView").rows[0].cells[10].innerText;
    }
    catch (e) {
        var tieudeFALSE = "Null";
    }

    //Lấy gía trị của biểu đồ
    try {
        var giatriFALSE = parseInt(document.getElementById("ExcelView").rows[6].cells[10].innerText);
    }
    catch (e) {
        var giatriFALSE = "Null";
    }


     chart12 = new CanvasJS.Chart("chartContainer12", {
        exportEnabled: true,
        animationEnabled: true,
        title: {
            text: titleTron1,
            fontFamily: "tahoma",
        },
        legend: {
            cursor: "pointer",
            itemclick: explodePie
        },
        data: [{
            type: "pie",
            showInLegend: true,
            toolTipContent: "{name}: <strong>{y}%</strong>",
            indexLabel: "{name} - {y}%",
            dataPoints: [
                { y: giatriTRUE, name: tieudeTRUE, exploded: true },
                { y: giatriFALSE, name: tieudeFALSE },


            ]
        }]
    });
    chart12.render();
}

//Phần Xuất Ra Biểu Đồ Dòng 7
function chartContainer13() {

    //Phần lấy ra Tiêu Đề
    var title = document.getElementById("ExcelView").rows[7].cells[3].innerText;
    var title1 = "Biểu Đồ Tổng Hợp SL Đi Phát Của " + title;

    //Lấy Thông Tin SL BƯU GỬI ĐẾN
    //Lấy tiêu đề của biểu đồ
    try {
        var a = document.getElementById("ExcelView").rows[0].cells[4].innerText;
    }
    catch (e) {
        var a = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var a1 = parseInt(document.getElementById("ExcelView").rows[7].cells[4].innerText);
    }
    catch (e) {
        var a1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT THÀNH CÔNG
    //Lấy tiêu đề của biểu đồ
    try {
        var b = document.getElementById("ExcelView").rows[0].cells[5].innerText;
    }
    catch (e) {
        var b = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var b1 = parseInt(document.getElementById("ExcelView").rows[7].cells[5].innerText);
    }
    catch (e) {
        var b1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT CHƯA CÓ THÔNG TIN
    //Lấy tiêu đề của biểu đồ
    try {
        var c = document.getElementById("ExcelView").rows[0].cells[6].innerText;
    }
    catch (e) {
        var c = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var c1 = parseInt(document.getElementById("ExcelView").rows[7].cells[6].innerText);
    }
    catch (e) {
        var c1 = "Null";
    }

    //Lấy Thông Tin SL PTC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var d = document.getElementById("ExcelView").rows[0].cells[7].innerText;
    }
    catch (e) {
        var d = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var d1 = parseInt(document.getElementById("ExcelView").rows[7].cells[7].innerText);

    }
    catch (e) {
        var d1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var e = document.getElementById("ExcelView").rows[0].cells[8].innerText;
    }
    catch (e) {
        var e = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var e1 = parseInt(document.getElementById("ExcelView").rows[7].cells[8].innerText);

    }
    catch (e) {
        var e1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG XÁC ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var f = document.getElementById("ExcelView").rows[0].cells[11].innerText;
    }
    catch (e) {
        var f = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var f1 = parseInt(document.getElementById("ExcelView").rows[7].cells[11].innerText);

    }
    catch (e) {
        var f1 = "Null";

    }


     chart13 = new CanvasJS.Chart("chartContainer13",
    {
        animationEnabled: true,
        title: {
            text: title1,
            fontFamily: "tahoma",
        },

        axisY: {
            title: title,
            //titleMaxWidth: 150 //**change the value of titleMaxWidth
        },

        data: [
        {
            type: "column",
            indexLabel: "{y}",
            dataPoints: [
            { label: a, y: a1 },
            { label: b, y: b1 },
            { label: c, y: c1 },
            { label: d, y: d1 },
            { label: e, y: e1 },
            { label: f, y: f1 },
            ]
        }
        ]
    });

    chart13.render();
}



function chartContainer14() {
    //Phần Xuất Ra Biểu Đồ Hình Tròn

    //Phần Lấy Ra Tiêu Đề Biểu Đồ Hình Tròn
    var titleTron = document.getElementById("ExcelView").rows[7].cells[3].innerText;
    var titleTron1 = "Tỉ Lệ PTC Của " + titleTron;

    //Lấy Thông Tin Tỉ Lệ TC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeTRUE = document.getElementById("ExcelView").rows[0].cells[9].innerText;
    }
    catch (e) {
        var tieudeTRUE = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var giatriTRUE = parseInt(document.getElementById("ExcelView").rows[7].cells[9].innerText);
    }
    catch (e) {
        var giatriTRUE = "Null";
    }

    //Lấy Thông Tin Tỉ Lệ TC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeFALSE = document.getElementById("ExcelView").rows[0].cells[10].innerText;
    }
    catch (e) {
        var tieudeFALSE = "Null";
    }

    //Lấy gía trị của biểu đồ
    try {
        var giatriFALSE = parseInt(document.getElementById("ExcelView").rows[7].cells[10].innerText);
    }
    catch (e) {
        var giatriFALSE = "Null";
    }


     chart14 = new CanvasJS.Chart("chartContainer14", {
        exportEnabled: true,
        animationEnabled: true,
        title: {
            text: titleTron1,
            fontFamily: "tahoma",
        },
        legend: {
            cursor: "pointer",
            itemclick: explodePie
        },
        data: [{
            type: "pie",
            showInLegend: true,
            toolTipContent: "{name}: <strong>{y}%</strong>",
            indexLabel: "{name} - {y}%",
            dataPoints: [
                { y: giatriTRUE, name: tieudeTRUE, exploded: true },
                { y: giatriFALSE, name: tieudeFALSE },


            ]
        }]
    });
    chart14.render();
}

//Phần Xuất Ra Biểu Đồ Dòng 8
function chartContainer15() {

    //Phần lấy ra Tiêu Đề
    var title = document.getElementById("ExcelView").rows[8].cells[3].innerText;
    var title1 = "Biểu Đồ Tổng Hợp SL Đi Phát Của " + title;

    //Lấy Thông Tin SL BƯU GỬI ĐẾN
    //Lấy tiêu đề của biểu đồ
    try {
        var a = document.getElementById("ExcelView").rows[0].cells[4].innerText;
    }
    catch (e) {
        var a = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var a1 = parseInt(document.getElementById("ExcelView").rows[8].cells[4].innerText);
    }
    catch (e) {
        var a1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT THÀNH CÔNG
    //Lấy tiêu đề của biểu đồ
    try {
        var b = document.getElementById("ExcelView").rows[0].cells[5].innerText;
    }
    catch (e) {
        var b = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var b1 = parseInt(document.getElementById("ExcelView").rows[8].cells[5].innerText);
    }
    catch (e) {
        var b1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT CHƯA CÓ THÔNG TIN
    //Lấy tiêu đề của biểu đồ
    try {
        var c = document.getElementById("ExcelView").rows[0].cells[6].innerText;
    }
    catch (e) {
        var c = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var c1 = parseInt(document.getElementById("ExcelView").rows[8].cells[6].innerText);
    }
    catch (e) {
        var c1 = "Null";
    }

    //Lấy Thông Tin SL PTC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var d = document.getElementById("ExcelView").rows[0].cells[7].innerText;
    }
    catch (e) {
        var d = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var d1 = parseInt(document.getElementById("ExcelView").rows[8].cells[7].innerText);

    }
    catch (e) {
        var d1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var e = document.getElementById("ExcelView").rows[0].cells[8].innerText;
    }
    catch (e) {
        var e = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var e1 = parseInt(document.getElementById("ExcelView").rows[8].cells[8].innerText);

    }
    catch (e) {
        var e1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG XÁC ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var f = document.getElementById("ExcelView").rows[0].cells[11].innerText;
    }
    catch (e) {
        var f = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var f1 = parseInt(document.getElementById("ExcelView").rows[8].cells[11].innerText);

    }
    catch (e) {
        var f1 = "Null";

    }


     chart15 = new CanvasJS.Chart("chartContainer15",
    {
        animationEnabled: true,
        title: {
            text: title1,
            fontFamily: "tahoma",
        },

        axisY: {
            title: title,
            //titleMaxWidth: 150 //**change the value of titleMaxWidth
        },

        data: [
        {
            type: "column",
            indexLabel: "{y}",
            dataPoints: [
            { label: a, y: a1 },
            { label: b, y: b1 },
            { label: c, y: c1 },
            { label: d, y: d1 },
            { label: e, y: e1 },
            { label: f, y: f1 },
            ]
        }
        ]
    });

    chart15.render();
}



function chartContainer16() {
    //Phần Xuất Ra Biểu Đồ Hình Tròn

    //Phần Lấy Ra Tiêu Đề Biểu Đồ Hình Tròn
    var titleTron = document.getElementById("ExcelView").rows[8].cells[3].innerText;
    var titleTron1 = "Tỉ Lệ PTC Của " + titleTron;

    //Lấy Thông Tin Tỉ Lệ TC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeTRUE = document.getElementById("ExcelView").rows[0].cells[9].innerText;
    }
    catch (e) {
        var tieudeTRUE = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var giatriTRUE = parseInt(document.getElementById("ExcelView").rows[8].cells[9].innerText);
    }
    catch (e) {
        var giatriTRUE = "Null";
    }

    //Lấy Thông Tin Tỉ Lệ TC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeFALSE = document.getElementById("ExcelView").rows[0].cells[10].innerText;
    }
    catch (e) {
        var tieudeFALSE = "Null";
    }

    //Lấy gía trị của biểu đồ
    try {
        var giatriFALSE = parseInt(document.getElementById("ExcelView").rows[8].cells[10].innerText);
    }
    catch (e) {
        var giatriFALSE = "Null";
    }


     chart16 = new CanvasJS.Chart("chartContainer16", {
        exportEnabled: true,
        animationEnabled: true,
        title: {
            text: titleTron1,
            fontFamily: "tahoma",
        },
        legend: {
            cursor: "pointer",
            itemclick: explodePie
        },
        data: [{
            type: "pie",
            showInLegend: true,
            toolTipContent: "{name}: <strong>{y}%</strong>",
            indexLabel: "{name} - {y}%",
            dataPoints: [
                { y: giatriTRUE, name: tieudeTRUE, exploded: true },
                { y: giatriFALSE, name: tieudeFALSE },


            ]
        }]
    });
    chart16.render();
}

//Phần Xuất Ra Biểu Đồ Dòng 9
function chartContainer17() {

    //Phần lấy ra Tiêu Đề
    var title = document.getElementById("ExcelView").rows[9].cells[3].innerText;
    var title1 = "Biểu Đồ Tổng Hợp SL Đi Phát Của " + title;

    //Lấy Thông Tin SL BƯU GỬI ĐẾN
    //Lấy tiêu đề của biểu đồ
    try {
        var a = document.getElementById("ExcelView").rows[0].cells[4].innerText;
    }
    catch (e) {
        var a = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var a1 = parseInt(document.getElementById("ExcelView").rows[9].cells[4].innerText);
    }
    catch (e) {
        var a1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT THÀNH CÔNG
    //Lấy tiêu đề của biểu đồ
    try {
        var b = document.getElementById("ExcelView").rows[0].cells[5].innerText;
    }
    catch (e) {
        var b = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var b1 = parseInt(document.getElementById("ExcelView").rows[9].cells[5].innerText);
    }
    catch (e) {
        var b1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT CHƯA CÓ THÔNG TIN
    //Lấy tiêu đề của biểu đồ
    try {
        var c = document.getElementById("ExcelView").rows[0].cells[6].innerText;
    }
    catch (e) {
        var c = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var c1 = parseInt(document.getElementById("ExcelView").rows[9].cells[6].innerText);
    }
    catch (e) {
        var c1 = "Null";
    }

    //Lấy Thông Tin SL PTC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var d = document.getElementById("ExcelView").rows[0].cells[7].innerText;
    }
    catch (e) {
        var d = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var d1 = parseInt(document.getElementById("ExcelView").rows[9].cells[7].innerText);

    }
    catch (e) {
        var d1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var e = document.getElementById("ExcelView").rows[0].cells[8].innerText;
    }
    catch (e) {
        var e = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var e1 = parseInt(document.getElementById("ExcelView").rows[9].cells[8].innerText);

    }
    catch (e) {
        var e1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG XÁC ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var f = document.getElementById("ExcelView").rows[0].cells[11].innerText;
    }
    catch (e) {
        var f = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var f1 = parseInt(document.getElementById("ExcelView").rows[9].cells[11].innerText);

    }
    catch (e) {
        var f1 = "Null";

    }


     chart17 = new CanvasJS.Chart("chartContainer17",
    {
        animationEnabled: true,
        title: {
            text: title1,
            fontFamily: "tahoma",
        },

        axisY: {
            title: title,
            //titleMaxWidth: 150 //**change the value of titleMaxWidth
        },

        data: [
        {
            type: "column",
            indexLabel: "{y}",
            dataPoints: [
            { label: a, y: a1 },
            { label: b, y: b1 },
            { label: c, y: c1 },
            { label: d, y: d1 },
            { label: e, y: e1 },
            { label: f, y: f1 },
            ]
        }
        ]
    });

    chart17.render();
}



function chartContainer18() {
    //Phần Xuất Ra Biểu Đồ Hình Tròn

    //Phần Lấy Ra Tiêu Đề Biểu Đồ Hình Tròn
    var titleTron = document.getElementById("ExcelView").rows[9].cells[3].innerText;
    var titleTron1 = "Tỉ Lệ PTC Của " + titleTron;

    //Lấy Thông Tin Tỉ Lệ TC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeTRUE = document.getElementById("ExcelView").rows[0].cells[9].innerText;
    }
    catch (e) {
        var tieudeTRUE = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var giatriTRUE = parseInt(document.getElementById("ExcelView").rows[9].cells[9].innerText);
    }
    catch (e) {
        var giatriTRUE = "Null";
    }

    //Lấy Thông Tin Tỉ Lệ TC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeFALSE = document.getElementById("ExcelView").rows[0].cells[10].innerText;
    }
    catch (e) {
        var tieudeFALSE = "Null";
    }

    //Lấy gía trị của biểu đồ
    try {
        var giatriFALSE = parseInt(document.getElementById("ExcelView").rows[9].cells[10].innerText);
    }
    catch (e) {
        var giatriFALSE = "Null";
    }


     chart18 = new CanvasJS.Chart("chartContainer18", {
        exportEnabled: true,
        animationEnabled: true,
        title: {
            text: titleTron1,
            fontFamily: "tahoma",
        },
        legend: {
            cursor: "pointer",
            itemclick: explodePie
        },
        data: [{
            type: "pie",
            showInLegend: true,
            toolTipContent: "{name}: <strong>{y}%</strong>",
            indexLabel: "{name} - {y}%",
            dataPoints: [
                { y: giatriTRUE, name: tieudeTRUE, exploded: true },
                { y: giatriFALSE, name: tieudeFALSE },


            ]
        }]
    });
    chart18.render();
}

//Phần Xuất Ra Biểu Đồ Dòng 10
function chartContainer19() {

    //Phần lấy ra Tiêu Đề
    var title = document.getElementById("ExcelView").rows[10].cells[3].innerText;
    var title1 = "Biểu Đồ Tổng Hợp SL Đi Phát Của " + title;

    //Lấy Thông Tin SL BƯU GỬI ĐẾN
    //Lấy tiêu đề của biểu đồ
    try {
        var a = document.getElementById("ExcelView").rows[0].cells[4].innerText;
    }
    catch (e) {
        var a = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var a1 = parseInt(document.getElementById("ExcelView").rows[10].cells[4].innerText);
    }
    catch (e) {
        var a1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT THÀNH CÔNG
    //Lấy tiêu đề của biểu đồ
    try {
        var b = document.getElementById("ExcelView").rows[0].cells[5].innerText;
    }
    catch (e) {
        var b = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var b1 = parseInt(document.getElementById("ExcelView").rows[10].cells[5].innerText);
    }
    catch (e) {
        var b1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT CHƯA CÓ THÔNG TIN
    //Lấy tiêu đề của biểu đồ
    try {
        var c = document.getElementById("ExcelView").rows[0].cells[6].innerText;
    }
    catch (e) {
        var c = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var c1 = parseInt(document.getElementById("ExcelView").rows[10].cells[6].innerText);
    }
    catch (e) {
        var c1 = "Null";
    }

    //Lấy Thông Tin SL PTC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var d = document.getElementById("ExcelView").rows[0].cells[7].innerText;
    }
    catch (e) {
        var d = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var d1 = parseInt(document.getElementById("ExcelView").rows[10].cells[7].innerText);

    }
    catch (e) {
        var d1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var e = document.getElementById("ExcelView").rows[0].cells[8].innerText;
    }
    catch (e) {
        var e = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var e1 = parseInt(document.getElementById("ExcelView").rows[10].cells[8].innerText);

    }
    catch (e) {
        var e1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG XÁC ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var f = document.getElementById("ExcelView").rows[0].cells[11].innerText;
    }
    catch (e) {
        var f = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var f1 = parseInt(document.getElementById("ExcelView").rows[10].cells[11].innerText);

    }
    catch (e) {
        var f1 = "Null";

    }


     chart19 = new CanvasJS.Chart("chartContainer19",
    {
        animationEnabled: true,
        title: {
            text: title1,
            fontFamily: "tahoma",
        },

        axisY: {
            title: title,
            //titleMaxWidth: 150 //**change the value of titleMaxWidth
        },

        data: [
        {
            type: "column",
            indexLabel: "{y}",
            dataPoints: [
            { label: a, y: a1 },
            { label: b, y: b1 },
            { label: c, y: c1 },
            { label: d, y: d1 },
            { label: e, y: e1 },
            { label: f, y: f1 },
            ]
        }
        ]
    });

    chart19.render();
}



function chartContainer20() {
    //Phần Xuất Ra Biểu Đồ Hình Tròn

    //Phần Lấy Ra Tiêu Đề Biểu Đồ Hình Tròn
    var titleTron = document.getElementById("ExcelView").rows[10].cells[3].innerText;
    var titleTron1 = "Tỉ Lệ PTC Của " + titleTron;

    //Lấy Thông Tin Tỉ Lệ TC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeTRUE = document.getElementById("ExcelView").rows[0].cells[9].innerText;
    }
    catch (e) {
        var tieudeTRUE = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var giatriTRUE = parseInt(document.getElementById("ExcelView").rows[10].cells[9].innerText);
    }
    catch (e) {
        var giatriTRUE = "Null";
    }

    //Lấy Thông Tin Tỉ Lệ TC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeFALSE = document.getElementById("ExcelView").rows[0].cells[10].innerText;
    }
    catch (e) {
        var tieudeFALSE = "Null";
    }

    //Lấy gía trị của biểu đồ
    try {
        var giatriFALSE = parseInt(document.getElementById("ExcelView").rows[10].cells[10].innerText);
    }
    catch (e) {
        var giatriFALSE = "Null";
    }


     chart20 = new CanvasJS.Chart("chartContainer20", {
        exportEnabled: true,
        animationEnabled: true,
        title: {
            text: titleTron1,
            fontFamily: "tahoma",
        },
        legend: {
            cursor: "pointer",
            itemclick: explodePie
        },
        data: [{
            type: "pie",
            showInLegend: true,
            toolTipContent: "{name}: <strong>{y}%</strong>",
            indexLabel: "{name} - {y}%",
            dataPoints: [
                { y: giatriTRUE, name: tieudeTRUE, exploded: true },
                { y: giatriFALSE, name: tieudeFALSE },


            ]
        }]
    });
    chart20.render();
}

//Phần Xuất Ra Biểu Đồ Dòng 11
function chartContainer21() {

    //Phần lấy ra Tiêu Đề
    var title = document.getElementById("ExcelView").rows[11].cells[3].innerText;
    var title1 = "Biểu Đồ Tổng Hợp SL Đi Phát Của " + title;

    //Lấy Thông Tin SL BƯU GỬI ĐẾN
    //Lấy tiêu đề của biểu đồ
    try {
        var a = document.getElementById("ExcelView").rows[0].cells[4].innerText;
    }
    catch (e) {
        var a = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var a1 = parseInt(document.getElementById("ExcelView").rows[11].cells[4].innerText);
    }
    catch (e) {
        var a1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT THÀNH CÔNG
    //Lấy tiêu đề của biểu đồ
    try {
        var b = document.getElementById("ExcelView").rows[0].cells[5].innerText;
    }
    catch (e) {
        var b = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var b1 = parseInt(document.getElementById("ExcelView").rows[11].cells[5].innerText);
    }
    catch (e) {
        var b1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT CHƯA CÓ THÔNG TIN
    //Lấy tiêu đề của biểu đồ
    try {
        var c = document.getElementById("ExcelView").rows[0].cells[6].innerText;
    }
    catch (e) {
        var c = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var c1 = parseInt(document.getElementById("ExcelView").rows[11].cells[6].innerText);
    }
    catch (e) {
        var c1 = "Null";
    }

    //Lấy Thông Tin SL PTC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var d = document.getElementById("ExcelView").rows[0].cells[7].innerText;
    }
    catch (e) {
        var d = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var d1 = parseInt(document.getElementById("ExcelView").rows[11].cells[7].innerText);

    }
    catch (e) {
        var d1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var e = document.getElementById("ExcelView").rows[0].cells[8].innerText;
    }
    catch (e) {
        var e = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var e1 = parseInt(document.getElementById("ExcelView").rows[11].cells[8].innerText);

    }
    catch (e) {
        var e1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG XÁC ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var f = document.getElementById("ExcelView").rows[0].cells[11].innerText;
    }
    catch (e) {
        var f = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var f1 = parseInt(document.getElementById("ExcelView").rows[11].cells[11].innerText);

    }
    catch (e) {
        var f1 = "Null";

    }


     chart21 = new CanvasJS.Chart("chartContainer21",
    {
        animationEnabled: true,
        title: {
            text: title1,
            fontFamily: "tahoma",
        },

        axisY: {
            title: title,
            //titleMaxWidth: 150 //**change the value of titleMaxWidth
        },

        data: [
        {
            type: "column",
            indexLabel: "{y}",
            dataPoints: [
            { label: a, y: a1 },
            { label: b, y: b1 },
            { label: c, y: c1 },
            { label: d, y: d1 },
            { label: e, y: e1 },
            { label: f, y: f1 },
            ]
        }
        ]
    });

    chart21.render();
}



function chartContainer22() {
    //Phần Xuất Ra Biểu Đồ Hình Tròn

    //Phần Lấy Ra Tiêu Đề Biểu Đồ Hình Tròn
    var titleTron = document.getElementById("ExcelView").rows[11].cells[3].innerText;
    var titleTron1 = "Tỉ Lệ PTC Của " + titleTron;

    //Lấy Thông Tin Tỉ Lệ TC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeTRUE = document.getElementById("ExcelView").rows[0].cells[9].innerText;
    }
    catch (e) {
        var tieudeTRUE = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var giatriTRUE = parseInt(document.getElementById("ExcelView").rows[11].cells[9].innerText);
    }
    catch (e) {
        var giatriTRUE = "Null";
    }

    //Lấy Thông Tin Tỉ Lệ TC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeFALSE = document.getElementById("ExcelView").rows[0].cells[10].innerText;
    }
    catch (e) {
        var tieudeFALSE = "Null";
    }

    //Lấy gía trị của biểu đồ
    try {
        var giatriFALSE = parseInt(document.getElementById("ExcelView").rows[11].cells[10].innerText);
    }
    catch (e) {
        var giatriFALSE = "Null";
    }


     chart22 = new CanvasJS.Chart("chartContainer22", {
        exportEnabled: true,
        animationEnabled: true,
        title: {
            text: titleTron1,
            fontFamily: "tahoma",
        },
        legend: {
            cursor: "pointer",
            itemclick: explodePie
        },
        data: [{
            type: "pie",
            showInLegend: true,
            toolTipContent: "{name}: <strong>{y}%</strong>",
            indexLabel: "{name} - {y}%",
            dataPoints: [
                { y: giatriTRUE, name: tieudeTRUE, exploded: true },
                { y: giatriFALSE, name: tieudeFALSE },


            ]
        }]
    });
    chart22.render();
}

//Phần Xuất Ra Biểu Đồ Dòng 12
function chartContainer23() {

    //Phần lấy ra Tiêu Đề
    var title = document.getElementById("ExcelView").rows[12].cells[3].innerText;
    var title1 = "Biểu Đồ Tổng Hợp SL Đi Phát Của " + title;

    //Lấy Thông Tin SL BƯU GỬI ĐẾN
    //Lấy tiêu đề của biểu đồ
    try {
        var a = document.getElementById("ExcelView").rows[0].cells[4].innerText;
    }
    catch (e) {
        var a = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var a1 = parseInt(document.getElementById("ExcelView").rows[12].cells[4].innerText);
    }
    catch (e) {
        var a1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT THÀNH CÔNG
    //Lấy tiêu đề của biểu đồ
    try {
        var b = document.getElementById("ExcelView").rows[0].cells[5].innerText;
    }
    catch (e) {
        var b = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var b1 = parseInt(document.getElementById("ExcelView").rows[12].cells[5].innerText);
    }
    catch (e) {
        var b1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT CHƯA CÓ THÔNG TIN
    //Lấy tiêu đề của biểu đồ
    try {
        var c = document.getElementById("ExcelView").rows[0].cells[6].innerText;
    }
    catch (e) {
        var c = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var c1 = parseInt(document.getElementById("ExcelView").rows[12].cells[6].innerText);
    }
    catch (e) {
        var c1 = "Null";
    }

    //Lấy Thông Tin SL PTC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var d = document.getElementById("ExcelView").rows[0].cells[7].innerText;
    }
    catch (e) {
        var d = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var d1 = parseInt(document.getElementById("ExcelView").rows[12].cells[7].innerText);

    }
    catch (e) {
        var d1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var e = document.getElementById("ExcelView").rows[0].cells[8].innerText;
    }
    catch (e) {
        var e = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var e1 = parseInt(document.getElementById("ExcelView").rows[12].cells[8].innerText);

    }
    catch (e) {
        var e1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG XÁC ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var f = document.getElementById("ExcelView").rows[0].cells[11].innerText;
    }
    catch (e) {
        var f = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var f1 = parseInt(document.getElementById("ExcelView").rows[12].cells[11].innerText);

    }
    catch (e) {
        var f1 = "Null";

    }


     chart23 = new CanvasJS.Chart("chartContainer23",
    {
        animationEnabled: true,
        title: {
            text: title1,
            fontFamily: "tahoma",
        },

        axisY: {
            title: title,
            //titleMaxWidth: 150 //**change the value of titleMaxWidth
        },

        data: [
        {
            type: "column",
            indexLabel: "{y}",
            dataPoints: [
            { label: a, y: a1 },
            { label: b, y: b1 },
            { label: c, y: c1 },
            { label: d, y: d1 },
            { label: e, y: e1 },
            { label: f, y: f1 },
            ]
        }
        ]
    });

    chart23.render();
}



function chartContainer24() {
    //Phần Xuất Ra Biểu Đồ Hình Tròn

    //Phần Lấy Ra Tiêu Đề Biểu Đồ Hình Tròn
    var titleTron = document.getElementById("ExcelView").rows[12].cells[3].innerText;
    var titleTron1 = "Tỉ Lệ PTC Của " + titleTron;

    //Lấy Thông Tin Tỉ Lệ TC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeTRUE = document.getElementById("ExcelView").rows[0].cells[9].innerText;
    }
    catch (e) {
        var tieudeTRUE = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var giatriTRUE = parseInt(document.getElementById("ExcelView").rows[12].cells[9].innerText);
    }
    catch (e) {
        var giatriTRUE = "Null";
    }

    //Lấy Thông Tin Tỉ Lệ TC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeFALSE = document.getElementById("ExcelView").rows[0].cells[10].innerText;
    }
    catch (e) {
        var tieudeFALSE = "Null";
    }

    //Lấy gía trị của biểu đồ
    try {
        var giatriFALSE = parseInt(document.getElementById("ExcelView").rows[12].cells[10].innerText);
    }
    catch (e) {
        var giatriFALSE = "Null";
    }


     chart24 = new CanvasJS.Chart("chartContainer24", {
        exportEnabled: true,
        animationEnabled: true,
        title: {
            text: titleTron1,
            fontFamily: "tahoma",
        },
        legend: {
            cursor: "pointer",
            itemclick: explodePie
        },
        data: [{
            type: "pie",
            showInLegend: true,
            toolTipContent: "{name}: <strong>{y}%</strong>",
            indexLabel: "{name} - {y}%",
            dataPoints: [
                { y: giatriTRUE, name: tieudeTRUE, exploded: true },
                { y: giatriFALSE, name: tieudeFALSE },


            ]
        }]
    });
    chart24.render();
}

//Phần Xuất Ra Biểu Đồ Dòng 13
function chartContainer25() {

    //Phần lấy ra Tiêu Đề
    var title = document.getElementById("ExcelView").rows[13].cells[3].innerText;
    var title1 = "Biểu Đồ Tổng Hợp SL Đi Phát Của " + title;

    //Lấy Thông Tin SL BƯU GỬI ĐẾN
    //Lấy tiêu đề của biểu đồ
    try {
        var a = document.getElementById("ExcelView").rows[0].cells[4].innerText;
    }
    catch (e) {
        var a = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var a1 = parseInt(document.getElementById("ExcelView").rows[13].cells[4].innerText);
    }
    catch (e) {
        var a1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT THÀNH CÔNG
    //Lấy tiêu đề của biểu đồ
    try {
        var b = document.getElementById("ExcelView").rows[0].cells[5].innerText;
    }
    catch (e) {
        var b = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var b1 = parseInt(document.getElementById("ExcelView").rows[13].cells[5].innerText);
    }
    catch (e) {
        var b1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT CHƯA CÓ THÔNG TIN
    //Lấy tiêu đề của biểu đồ
    try {
        var c = document.getElementById("ExcelView").rows[0].cells[6].innerText;
    }
    catch (e) {
        var c = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var c1 = parseInt(document.getElementById("ExcelView").rows[13].cells[6].innerText);
    }
    catch (e) {
        var c1 = "Null";
    }

    //Lấy Thông Tin SL PTC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var d = document.getElementById("ExcelView").rows[0].cells[7].innerText;
    }
    catch (e) {
        var d = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var d1 = parseInt(document.getElementById("ExcelView").rows[13].cells[7].innerText);

    }
    catch (e) {
        var d1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var e = document.getElementById("ExcelView").rows[0].cells[8].innerText;
    }
    catch (e) {
        var e = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var e1 = parseInt(document.getElementById("ExcelView").rows[13].cells[8].innerText);

    }
    catch (e) {
        var e1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG XÁC ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var f = document.getElementById("ExcelView").rows[0].cells[11].innerText;
    }
    catch (e) {
        var f = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var f1 = parseInt(document.getElementById("ExcelView").rows[13].cells[11].innerText);

    }
    catch (e) {
        var f1 = "Null";

    }


     chart25 = new CanvasJS.Chart("chartContainer25",
    {
        animationEnabled: true,
        title: {
            text: title1,
            fontFamily: "tahoma",
        },

        axisY: {
            title: title,
            //titleMaxWidth: 150 //**change the value of titleMaxWidth
        },

        data: [
        {
            type: "column",
            indexLabel: "{y}",
            dataPoints: [
            { label: a, y: a1 },
            { label: b, y: b1 },
            { label: c, y: c1 },
            { label: d, y: d1 },
            { label: e, y: e1 },
            { label: f, y: f1 },
            ]
        }
        ]
    });

    chart25.render();
}



function chartContainer26() {
    //Phần Xuất Ra Biểu Đồ Hình Tròn

    //Phần Lấy Ra Tiêu Đề Biểu Đồ Hình Tròn
    var titleTron = document.getElementById("ExcelView").rows[13].cells[3].innerText;
    var titleTron1 = "Tỉ Lệ PTC Của " + titleTron;

    //Lấy Thông Tin Tỉ Lệ TC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeTRUE = document.getElementById("ExcelView").rows[0].cells[9].innerText;
    }
    catch (e) {
        var tieudeTRUE = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var giatriTRUE = parseInt(document.getElementById("ExcelView").rows[13].cells[9].innerText);
    }
    catch (e) {
        var giatriTRUE = "Null";
    }

    //Lấy Thông Tin Tỉ Lệ TC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeFALSE = document.getElementById("ExcelView").rows[0].cells[10].innerText;
    }
    catch (e) {
        var tieudeFALSE = "Null";
    }

    //Lấy gía trị của biểu đồ
    try {
        var giatriFALSE = parseInt(document.getElementById("ExcelView").rows[13].cells[10].innerText);
    }
    catch (e) {
        var giatriFALSE = "Null";
    }


     chart26 = new CanvasJS.Chart("chartContainer26", {
        exportEnabled: true,
        animationEnabled: true,
        title: {
            text: titleTron1,
            fontFamily: "tahoma",
        },
        legend: {
            cursor: "pointer",
            itemclick: explodePie
        },
        data: [{
            type: "pie",
            showInLegend: true,
            toolTipContent: "{name}: <strong>{y}%</strong>",
            indexLabel: "{name} - {y}%",
            dataPoints: [
                { y: giatriTRUE, name: tieudeTRUE, exploded: true },
                { y: giatriFALSE, name: tieudeFALSE },


            ]
        }]
    });
    chart26.render();
}

//Phần Xuất Ra Biểu Đồ Dòng 14
function chartContainer27() {

    //Phần lấy ra Tiêu Đề
    var title = document.getElementById("ExcelView").rows[14].cells[3].innerText;
    var title1 = "Biểu Đồ Tổng Hợp SL Đi Phát Của " + title;

    //Lấy Thông Tin SL BƯU GỬI ĐẾN
    //Lấy tiêu đề của biểu đồ
    try {
        var a = document.getElementById("ExcelView").rows[0].cells[4].innerText;
    }
    catch (e) {
        var a = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var a1 = parseInt(document.getElementById("ExcelView").rows[14].cells[4].innerText);
    }
    catch (e) {
        var a1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT THÀNH CÔNG
    //Lấy tiêu đề của biểu đồ
    try {
        var b = document.getElementById("ExcelView").rows[0].cells[5].innerText;
    }
    catch (e) {
        var b = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var b1 = parseInt(document.getElementById("ExcelView").rows[14].cells[5].innerText);
    }
    catch (e) {
        var b1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT CHƯA CÓ THÔNG TIN
    //Lấy tiêu đề của biểu đồ
    try {
        var c = document.getElementById("ExcelView").rows[0].cells[6].innerText;
    }
    catch (e) {
        var c = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var c1 = parseInt(document.getElementById("ExcelView").rows[14].cells[6].innerText);
    }
    catch (e) {
        var c1 = "Null";
    }

    //Lấy Thông Tin SL PTC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var d = document.getElementById("ExcelView").rows[0].cells[7].innerText;
    }
    catch (e) {
        var d = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var d1 = parseInt(document.getElementById("ExcelView").rows[14].cells[7].innerText);

    }
    catch (e) {
        var d1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var e = document.getElementById("ExcelView").rows[0].cells[8].innerText;
    }
    catch (e) {
        var e = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var e1 = parseInt(document.getElementById("ExcelView").rows[14].cells[8].innerText);

    }
    catch (e) {
        var e1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG XÁC ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var f = document.getElementById("ExcelView").rows[0].cells[11].innerText;
    }
    catch (e) {
        var f = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var f1 = parseInt(document.getElementById("ExcelView").rows[14].cells[11].innerText);

    }
    catch (e) {
        var f1 = "Null";

    }


     chart27 = new CanvasJS.Chart("chartContainer27",
    {
        animationEnabled: true,
        title: {
            text: title1,
            fontFamily: "tahoma",
        },

        axisY: {
            title: title,
            //titleMaxWidth: 150 //**change the value of titleMaxWidth
        },

        data: [
        {
            type: "column",
            indexLabel: "{y}",
            dataPoints: [
            { label: a, y: a1 },
            { label: b, y: b1 },
            { label: c, y: c1 },
            { label: d, y: d1 },
            { label: e, y: e1 },
            { label: f, y: f1 },
            ]
        }
        ]
    });

    chart27.render();
}



function chartContainer28() {
    //Phần Xuất Ra Biểu Đồ Hình Tròn

    //Phần Lấy Ra Tiêu Đề Biểu Đồ Hình Tròn
    var titleTron = document.getElementById("ExcelView").rows[14].cells[3].innerText;
    var titleTron1 = "Tỉ Lệ PTC Của " + titleTron;

    //Lấy Thông Tin Tỉ Lệ TC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeTRUE = document.getElementById("ExcelView").rows[0].cells[9].innerText;
    }
    catch (e) {
        var tieudeTRUE = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var giatriTRUE = parseInt(document.getElementById("ExcelView").rows[14].cells[9].innerText);
    }
    catch (e) {
        var giatriTRUE = "Null";
    }

    //Lấy Thông Tin Tỉ Lệ TC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeFALSE = document.getElementById("ExcelView").rows[0].cells[10].innerText;
    }
    catch (e) {
        var tieudeFALSE = "Null";
    }

    //Lấy gía trị của biểu đồ
    try {
        var giatriFALSE = parseInt(document.getElementById("ExcelView").rows[14].cells[10].innerText);
    }
    catch (e) {
        var giatriFALSE = "Null";
    }


     chart28 = new CanvasJS.Chart("chartContainer28", {
        exportEnabled: true,
        animationEnabled: true,
        title: {
            text: titleTron1,
            fontFamily: "tahoma",
        },
        legend: {
            cursor: "pointer",
            itemclick: explodePie
        },
        data: [{
            type: "pie",
            showInLegend: true,
            toolTipContent: "{name}: <strong>{y}%</strong>",
            indexLabel: "{name} - {y}%",
            dataPoints: [
                { y: giatriTRUE, name: tieudeTRUE, exploded: true },
                { y: giatriFALSE, name: tieudeFALSE },


            ]
        }]
    });
    chart28.render();
}

function explodePie(e) {
    if (typeof (e.dataSeries.dataPoints[e.dataPointIndex].exploded) === "undefined" || !e.dataSeries.dataPoints[e.dataPointIndex].exploded) {
        e.dataSeries.dataPoints[e.dataPointIndex].exploded = true;
    } else {
        e.dataSeries.dataPoints[e.dataPointIndex].exploded = false;
    }
    e.chart.render();

}
