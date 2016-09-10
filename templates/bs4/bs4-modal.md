---
guid: ccea6e52-e82f-4951-ae78-6647684e5fa1
type: Live
reformat: True
shortenReferences: True
categories: bootstrap 4
scopes: InHtmlLikeFile
parameterOrder: id, title, close, save
id-expression: constant("id")
title-expression: constant("Modal title")
close-expression: constant("Close")
save-expression: constant("Save")
---

# bs4-modal

Modal dialog

```
<a class="btn btn-primary" data-toggle="modal" href="#$id$">Trigger modal</a>
<div class="modal fade" id="$id$">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
		        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
		          <span aria-hidden="true">&times;</span>
		        </button>
                <h4 class="modal-title">$title$</h4>
            </div>
            <div class="modal-body">
                $END$
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">$close$</button>
                <button type="button" class="btn btn-primary">$save$</button>
            </div>
        </div>
    </div>
</div>
```
