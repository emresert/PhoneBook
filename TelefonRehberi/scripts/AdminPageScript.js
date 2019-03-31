//Personel  Delete Part
$(function () {
    $("#tblPersonel").on("click", ".personelDelete", function () {
        var btn = $(this);
        bootbox.confirm("Personeli silmek istediğinize emin misiniz?",function(result){
            if (result) {
                var id = btn.data("id");
             
                $.ajax({
                    type: "GET",
                    url: "/AdminUI/PersonelDelete/" + id,
                    success: function () {
                        bootbox.alert("Kayıt silindi");
                        btn.parent().parent().remove();
                        
                    },
                    error: function () {
                        bootbox.alert("Bu personel başka bir personelin yöneticisi olduğu için silinemez.");
                    }
                });
            }
           
        })
    });
});

// Departman Delete Part

$(function () {
    $("#tblDepartman").on("click", ".departmanDelete", function () {
        var btndep = $(this);
        bootbox.confirm("Departmanı silmek istediğinize emin misiniz?", function (result) {
            if (result) {
                var id = btndep.data("id");

                $.ajax({
                    type: "GET",
                    url: "/AdminUI/DepartmanDelete/" + id,
                    success: function () {
                        bootbox.alert("Kayıt silindi");
                         btndep.parent().parent().remove();
                                                 
                    },
                    error: function () {
                        bootbox.alert("Departmanda çalışan personeller olduğu için silemezsiniz.");
                    }
                });
            }

        })
    });
});