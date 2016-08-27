---
guid: bf365f2b-5b2a-406f-937c-6dc1dc3d5022
type: Live
reformat: True
shortenReferences: True
categories: bootstrap 3
scopes: InHtmlLikeFile
parameterOrder: id, type, name, value, required
id-expression: constant("id")
name-expression: constant("name")
value-expression: constant("value")
required-expression: constant("required")
type-expression: list("color,date,email,hidden,month,number,password,range,search,tel,text,url,week")
---

# bs3-input-h

Input horizontal form

```
<div class="form-group">
    <label for="$id$" class="col-sm-2 control-label">$label$</label>
    <div class="col-sm-10">
        <input type="$type$" name="" id="$id$" class="form-control" value="$value$" $required$ title="">
    </div>
</div>
```
