---
guid: bc3fb479-1b56-4084-b871-9916cff7af72
type: Live
reformat: True
shortenReferences: True
categories: bootstrap 3
scopes: InHtmlLikeFile
parameterOrder: action, method, submit
action-expression: constant("action")
method-expression: constant("POST")
submit-expression: constant("Submit")
---

# bs3-form-inline



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
