
<div id="divQuoiDeNeuf" style="overflow: auto;">
    <h3>Grille des examens</h3>
    <p>
        En cliquant dans la case de la tonalité, il est possible d'entrer ou de modifier directement la donnée.
    </p>
    <p>
        Je vous rappel que les tonalités sont propres à chaque utilisateur. Seulement vous voyez ce que vous avec saisi.
        Ne vous gênez donc pas pour vous en servir.
    </p>

    <h3>Onglet pour les sélections de chants</h3>
    <p>
        Les listes de chants sont aussi propres à chaque utilisateur. Vous ne voyez que les listes que vous avez crée.
    </p>
    <p>
        La première étape pour faire une liste est de sélectionner la date.
        Par défaut c'est la date du jour alors les chants d'aujourd'hui seront automatiquement chargés.
        En sélectionnant une nouvelle date, par exemple pour dimanche prochain,
        les chants que vous aurez entré pour cette date se chargeront automatiquement.
        Il sera alors possible de d'apporter des modifications à cette liste.
    </p>
    <p>
        Pour ajouter un chant à la liste, il suffit de faire glisser un chant de la grille de recherche directement dans la section souhaité.
        Le chant s'ajoutera à la fin de cette section.
        Chaque chant de la liste peut à son tour être déplacé en le glissant pour changer l'ordre.
        Il est possible de le déplacer dans une autre section, par exemple prendre un chant de la section "Pendant la réunion", et le
        faire glisser dans lea section "Avant la réunion".
    </p>
    <p>
        Pour retirer un chant de la liste, il suffit de le faire glisser hors de la zone de sélection.
    </p>
    <p>
        Il est aussi possible de saisir la tonalité directement dans la zone de sélection.
        La tonalité sera conservé pour le chant et sera disponible lors de la prochaine sélection de ce chant.
    </p>
    <p>
        Plus besoin de sauvegarder vos listes, tout s'enregistre automatiquement.
    </p>
    <p>
        Le bouton "Télécharger" permet de récupérer le document Word contenant la liste sélectionné.
        Plus besoin de configurer les paramètres de sécurité car le document est généré par l'application et non sur vos ordinateurs.
        Vous n'avez qu'à sauvegarder le fichier téléchargé pour l'imprimer ou le publier.
    </p>

</div>

<script type="text/javascript">

    $(document).ready(function () {

        function ResizeQuoiDeNeuf() {
            $("#divQuoiDeNeuf").height($(window).height() - 190);
        }

        ResizeQuoiDeNeuf();

        $(window).resize(function () {
            ResizeQuoiDeNeuf();
        });

    });


</script>