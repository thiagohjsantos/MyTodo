$(document).ready(function(){
    $('#myInput').on("keyup",function(){
        var value =$(this).val().toLocaleLowerCase();
        $("#tableData tr").filter(function(){
            $(this).toggle($(this).text().toLocaleLowerCase().indexOf(value)>-1)
        })
    });
    $('#buttonPending').css({"background-color": "#eb6864", "color": "white"});
})

$('#buttonPending, #buttonCompleted, #buttonAll').click(function () {
    if (this.id == 'buttonPending') {
        $('#Pending').show();
        $('#buttonPending').css({"background-color": "#eb6864", "color": "white"});
        $('#buttonCompleted').css({"background-color": "", "color": ""});
        $('#buttonAll').css({"background-color": "", "color": ""});
        $('#Completed').css("display","none");
        $('#All').css("display","none");
    }
    else if (this.id == 'buttonCompleted') {
        $('#Completed').show();
        $('#buttonPending').css({"background-color": "", "color": ""});
        $('#buttonCompleted').css({"background-color": "#02a602", "color": "white"});
        $('#buttonAll').css({"background-color": "", "color": ""});
        $('#All').css("display","none");
        $('#Pending').css("display","none");
    }
    else if(this.id == 'buttonAll') {
        $('#All').show();
        $('#buttonPending').css({"background-color": "", "color": ""});
        $('#buttonCompleted').css({"background-color": "", "color": ""});
        $('#buttonAll').css({"background-color": "#6aa2d0", "color": "white"});
        $('#Completed').css("display","none");
        $('#Pending').css("display","none");
    }
});