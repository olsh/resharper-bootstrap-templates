---
guid: 296560df-92ed-4e37-9120-dace51ddffaf
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
    <div class="form-control">
        <label for="">$label$</label>
        <input type="text" class="form-control" id="" placeholder="Input field">
    </div>
    $END$
    <button type="submit" class="btn btn-primary">$submit$</button>
</form>
```
