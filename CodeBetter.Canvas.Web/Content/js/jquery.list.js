$(document).ready(function()
{
    var $listRows =  $('table.list tbody tr'); 
    $listRows.filter(':odd').addClass('odd');
    $listRows.filter('[rel]').click(function()
    {
        var $this = $(this);        
        top.location = $this.closest('table').attr('rel') + '/' + $this.attr('rel');
    }).hoverToggle('hover');
    $('.pager a.page').click(function()
    {                  
        var r = /\/\d+/;
        var isPaged = r.test(top.location.pathname);
        var url = isPaged ? top.location.pathname.replace(r, '/' + $(this).text()) : top.location.pathname + '/' + $(this).text();
        top.location = url;
    });
});    