---
guid: a2068ec5-1728-4754-99e1-d9a1f632d0d6
type: Live
reformat: True
shortenReferences: True
categories: bootstrap 4
scopes: InHtmlLikeFile
parameterOrder: action, method, title, label, submit
action-expression: constant("")
method-expression: constant("POST")
title-expression: constant("Form title")
label-expression: constant("label")
submit-expression: constant("Submit")
---

# bs4-form

Form

```
<form action="$action$" method="$method$" role="form">
    <legend>$title$</legend>
    <div class="form-group">
        <label for="">$label$</label>
        <input type="text" class="form-control" id="" placeholder="Input field">
    </div>
    $END$
    <button type="submit" class="btn btn-primary">$submit$</button>
</form>
```
