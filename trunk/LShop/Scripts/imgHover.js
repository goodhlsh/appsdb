/* 滑过图片出现放大镜效果 */
$(function() {
    $("#jnBrandList li").each(function(index) {
        var $img = $(this).find("img");
        var imgW = $img.width();
        var imgH = $img.height();
        var spanHtml = '<span style="position:absolute;top:0;left:5px;width:' + imgW + 'px;height:' + imgH + 'px;" class="imageMask"></span>';
        $(spanHtml).appendTo(this);
    });
	$("#jnBrandList").delegate(".imageMask", "hover", function(){
		$(this).toggleClass("imageOver");
	});
	
	/*
	$("#jnBrandList").find(".imageMask").live("hover", function(){
		$(this).toggleClass("imageOver");
	});
	*/
})