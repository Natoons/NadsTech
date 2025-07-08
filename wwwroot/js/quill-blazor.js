window.QuillBlazor = {
    editors: {},
    
    create: function (elementId, dotNetRef, content) {
        // Attendre que l'élément soit disponible
        setTimeout(function() {
            var element = document.getElementById(elementId);
            if (!element) {
                console.error('Element not found:', elementId);
                return;
            }
            
            // Vérifier que Quill est chargé
            if (typeof Quill === 'undefined') {
                console.error('Quill is not loaded');
                return;
            }
            
            var quill = new Quill('#' + elementId, {
                theme: 'snow',
                modules: {
                    toolbar: [
                        [{ header: [1, 2, false] }],
                        ['bold', 'italic', 'underline'],
                        ['image', 'code-block'],
                        [{ list: 'ordered' }, { list: 'bullet' }],
                        ['clean']
                    ]
                }
            });
            
            if (content) {
                quill.root.innerHTML = content;
            }
            
            if (dotNetRef) {
                quill.on('text-change', function () {
                    dotNetRef.invokeMethodAsync('OnQuillContentChanged', quill.root.innerHTML);
                });
            }
            
            this.editors[elementId] = quill;
            console.log('Quill editor created for:', elementId);
        }, 100);
    },
    
    setContent: function (elementId, content) {
        var quill = this.editors[elementId];
        if (quill) {
            quill.root.innerHTML = content;
        }
    },
    
    getContent: function (elementId) {
        var quill = this.editors[elementId];
        if (quill) {
            return quill.root.innerHTML;
        }
        return '';
    }
}; 