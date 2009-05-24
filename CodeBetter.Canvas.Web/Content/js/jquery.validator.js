(function($)
{
    $.fn.validate = function(options, command)
    {
        var defaults = { showTip: true };
        options = $.extend(defaults, options);
        var rules = options.rules;        
        
        return this.each(function()
        {
            if (this.validator) { return false; }
            var $form = $(this);    
            var $fields = $('input,select,textarea', $form);
            
            var v =
            {
                initialize: function()
                {                    
                    $fields.each(function(i, field)
                    {
                        var $field = $(field);
                        $field.blur(function()
                        {                                       
                            v.validateField(this);
                        });                          
                        if (options.init && options.init[field.id] != null)                        
                        {                            
                            v.markAsInvalid($field, options.init[field.id] != '' ? options.init[field.id] : rules[field.id].tip);
                        }   
                    });
                    $form.submit(function()
                    {                                                                    
                        var isValid = true;                        
                        $fields.each(function(i, field)
                        {                                                                
                            if (!v.validateField(field) && isValid) 
                            { 
                                isValid = false;
                                $(field).focus();
                            }
                        });                                                
                        return isValid;
                    });                    
                },
                validateField: function(field)
                {                                                 
                    var rule = rules[field.id];
                    if (!rule) { return true; }                    
                    var $field = $(field);
                    var value = $field.val();
                    var isValid = true;                    
                    if (rule.required && value.length == 0) { isValid = false ; }
                    else if (rule.min && rule.min > value.length) { isValid = false; }
                    else if (rule.max && rule.max < value.length) { isValid = false; }
                    else if (rule.pattern && !rule.pattern.test(value)) { isValid = false; }
                    else if (rule.comp_eq && $(rule.comp_eq).val() != value) { isValid = false; }
                    if (!isValid)
                    {                        
                        v.markAsInvalid($field, rule.tip);                        
                    }
                    else
                    {
                        v.markAsValid($field);                        
                    }
                    return isValid;
                    
                },   
                showFormError: function(message)
                {
                    $form.prepend('<div class="mainError">' + message + '</div>');
                },         
                markAsInvalid: function($field, tip)
                {                                    
                    if (options.showTip && tip)
                    {
                        var $tip = $field.siblings('.tip');
                        if ($tip.length == 0)
                        {                        
                            $tip = $('<label>').attr('for', $field.attr('name')).addClass('tip').insertAfter($field);                        
                        }
                        $tip.html(tip).fadeIn();
                    }
                    $field.addClass('error');
                },
                markAsValid: function($field)
                {
                    $field.siblings('.tip').fadeOut();
                    $field.removeClass('error');
                }  
            }                                 
            this.validator = v;
            v.initialize();         

        });
    };
})(jQuery);