---
guid: b0226462-dbff-491e-b432-6afbc568ba93
type: Live
reformat: True
shortenReferences: True
categories: bootstrap 3
scopes: InHtmlLikeFile
parameterOrder: action, method, title, submit
action-expression: constant("")
method-expression: constant("POST")
title-expression: constant("Form title")
submit-expression: constant("Submit")
---

# bs3-form-horizontal



```
<form action="$action$" method="$method$" class="form-horizontal" role="form">
        <div class="form-group">
            <legend>$title$</legend>
        </div>
        $END$
        <div class="form-group">
            <div class="col-sm-10 col-sm-offset-2">
                <button type="submit" class="btn btn-primary">$submit$</button>
            </div>
        </div>
</form>
```
