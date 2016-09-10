---
guid: c003b732-f516-447e-8abb-05a8f9589162
type: Live
reformat: True
shortenReferences: True
categories: bootstrap 4
scopes: InHtmlLikeFile
parameterOrder: action, method, submit
action-expression: constant("action")
method-expression: constant("POST")
submit-expression: constant("Submit")
---

# bs4-form-inline

Inline form

```
<form action="$action$" method="$method$" class="form-inline" role="form">
    <div class="form-group">
        <label class="sr-only" for="">label</label>
        <input type="email" class="form-control" id="" placeholder="Input field">
    </div>
    $END$
    <button type="submit" class="btn btn-primary">$submit$</button>
</form>
```
