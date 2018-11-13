//ensure bundle is available

// invoke jquery and pass in object/DOM and html all ready to go

$(function () {

    //function to handle form submission. 
    var ajaxFormSubmit = function () {


        //reference to submitted form within event handler
        var $form = $(this);

        //build options matched up with form attributes in cshtml
        var options = {
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize()

        };


        //make ASync call using $.ajax-- pass above OPTIONS object in..
        

        $.ajax(options).done(function (data)   //response from server is the DATA object.
        {
            var $target = $($form.attr("data-rmdb-target"));        //$target gets target/DOM element from identifier
            var $newHtml = $(data);                  
            $target.replaceWith($newHtml);                                 //replace target with html data back from server
            $newHtml.find("div.ResultHighlightRibbon").effect("highlight");                      //WHY DOES THIS ONLY WORK WITH -- <section class="content-wrapper main-content clear-fix">
       
        });

        return false;                                            //prevent browser default of redrawing page by return false?                                                

    };
        

    var createAutocomplete = function () {

        var $input = $(this);   //passes in input wrapped in jquery
        console.log($input);

        var options = {
            source: $input.attr("data-rmdb-autocomplete"),  //options identifies source of data
            select: submitAutocompleteForm  //defines function to perform on select


        };

        $input.autocomplete(options);

     

    };


    var submitAutocompleteForm = function (event, ui) {    //autocomplete passes in UI paramater - 

        var $input = $(this);   //wrap DOM in jQuery 

        $input.val(ui.item.label);    // autocomplete passes in UI paramater // set value manually to clear old value!!

        var $form = $input.parents("form:first"); // find first form from parents in the DOM (in case nested form)
        $form.submit();  //submit event

    };
    

    var getPage = function () {

        var $a = $(this); //references clicked anchor tag 

        var options = {                                                 //extract options from anchor tag

            url: $a.attr("href"),               //link info for destination page
            data: $("form".serialize) ,          //get data from form and send along to track name in search
            type: "get"                         // request type
            
        };


        $.ajax(options).done(function (data) {                 //pump options into ajax Async request  and DONE output DATA object

            var target = $a.parents("div.pagedList").attr("data-rmdb-target");  // target Anchors "pagedList Div" attribute and "data target"" attr? IE EXTRACTS RIDERLIST
            var $newHtml = $(data);      
            $(target).replaceWith($newHtml);    //replace target(s) with data from ajax method            
            $newHtml.find("div.ResultHighlightRibbon").effect("highlight");  


        });

        return false; //dont default and refresh entire page
    };


    //find all of these labelled form elements and submit -  saves or submits come to this js instead of submitted to server
    $("form[data-rmdb-ajax='true']").submit(ajaxFormSubmit);

    //find inputs with data-rmdb-autocomplete and for each create autocomplete/give widget
    $("input[data-rmdb-autocomplete]").each(createAutocomplete);


    //Main content class is never destroyed.. is part of layout and present every page
    $(".main-content").on("click",".pagedList a", getPage);   //only register click events on main content and filter to pagedList anchor tags.

 });