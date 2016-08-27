---
guid: 31c92c18-ef81-4d77-b33c-6c148fa89ff4
type: Live
reformat: True
shortenReferences: True
categories: bootstrap 3
scopes: InHtmlLikeFile
parameterOrder: href, src, alt, header, text
href-expression: constant("#")
src-expression: constant("#")
alt-expression: constant("Image")
header-expression: constant("Media heading")
text-expression: constant("Text goes here...")
---

# bs3-media

Media object

```
<div class="media">
    <a class="pull-left" href="$href$">
        <img class="media-object" src="$src$" alt="$alt$">
    </a>
    <div class="media-body">
        <h4 class="media-heading">$header$</h4>
        <p>$text$</p>
    </div>
</div>
```
