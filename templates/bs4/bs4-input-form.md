---
guid: d6fa1399-c715-4c03-9a4b-4035b6b1b04c
type: Live
reformat: True
shortenReferences: True
categories: bootstrap 4
scopes: InHtmlLikeFile
parameterOrder: row, id, label, type, name, value, required
id-expression: constant("id")
name-expression: constant("name")
value-expression: constant("value")
required-expression: constant("required")
type-expression: list("color,date,email,hidden,month,number,password,range,search,tel,text,url,week")
row-expression: list(",row")
---

# bs4-input-form

Input horizontal form

```
<div class="form-group">
    <label for="$id$" class="col-sm-2 form-control-label">$label$</label>
    <div class="col-sm-10">
        <input type="$type$" name="" id="$id$" class="form-control" value="$value$" $required$ title="">
    </div>
</div>
```
