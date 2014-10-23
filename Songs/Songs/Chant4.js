// JScript File
        var oldTR;
        var sourceElem;

        function NouvelleListe() {
            window.open("../Songs/DatePicker.aspx", null, "height=260,width=300,status=no,toolbar=no,menubar=no,location=no").focus();

            var laDate;
            laDate = showModalDialog("../Songs/DatePicker.aspx", null, "help:0; status:0; dialogWidth: 300px; dialogHeight: 260px");
            if (laDate) {
                form1.txtDateSel.value = laDate;
                ViderSelection();
            }
        }
        
        function ShowTop10(user_id)
        {
            window.open("../Songs/Top10.aspx?USER_ID=" + user_id, "TOP10" + user_id, "height=400,width=500,status=no,toolbar=no,menubar=no,location=no").focus();
        }
        
        function GestionUtilisateurs()
        {
            window.open("../Users/GestionUsers.aspx", null, "width=500, height=500, status=no,toolbar=no,menubar=no,location=no").focus();
        }
        
        function GestionThemes()
        {
            window.open("../Ref/GestionThemes.aspx", null, "width=400, height=300, status=no,toolbar=no,menubar=no,location=no").focus();
        }
        
        function UpdateSong(song_id)
        {
            window.open("UpdateSong.aspx?SONG_ID=" + song_id, null, "width=800, height=450, status=no,toolbar=no,menubar=no,location=no,center=1").focus();
        }
        
        function PreDeleteSong(song_id)
        {
            //Wait to let the line highlight
            setTimeout("DeleteSong(" + song_id + ")", 10, "javascript");
        }
        function DeleteSong(song_id)
        {
            if (confirm("Voulez-vous réellement supprimer le chant sélectionné?\nAttention, la suppression est définitive."))
            {
                form1.txtDelSong.value = song_id;
                form1.btnDelSong.click();
            }
        }
        
        function OnSelectTR(aTR)
        {
            if (aTR == null || aTR.style == null)
            {
                aTR = window.event.srcElement;
                while (aTR.tagName != "TR")
                  aTR = aTR.parentElement;
            }

            if (oldTR != null)
            {
              oldTR.style.backgroundColor = "White";
              oldTR.style.color = "navy";
            }
            oldTR = aTR;   
            aTR.style.backgroundColor = "yellow";
            //aTR.style.color = "white";
            
            lblParoles.style.height = divParoles.clientHeight;

            document.getElementById("txtSondId").value = aTR.cells.item(0).innerText;
            document.getElementById("btnUpdateSong").click();
        }
        
        function OnResize() {
            //alert(document.documentElement.clientHeight);
            //alert(document.body.clientHeight);
            
            var refHeight = document.documentElement.clientHeight;
            var buffer = 16;
            var obj = divResultat;
            while (obj != document.body) {
                buffer += obj.offsetTop;
                obj = obj.offsetParent;
            }
            
            if (refHeight > buffer) {
                divResultat.style.height = refHeight - buffer + "px";
                divParoles.style.height = refHeight - buffer + 8 + "px";
                divAccords.style.height = refHeight - buffer - 36 + "px";
                if (parseInt(divResultat.style.height, 10) > 40)
                    divSelection.style.height = parseInt(divResultat.style.height, 10) - 40 + "px";
            }
        }
        
        function SetSelectionData() {
            var strData = '';
            tblMain.rows(1).cells(1).innerText;
            var tables = divSelection.getElementsByTagName("table");
            for (var i = 0; i < tables.length; i++) {
                if (i > 0)
                    strData += '§';
                for (var j = 1; j < tables(i).rows.length; j++) {
                    if (j > 1)
                        strData += '¦';
                    for (var k = 0; k < tables(i).rows(j).cells.length; k++) {
                        if (k > 0)
                            strData += '¤';
                        strData += tables(i).rows(j).cells(k).innerText;
                    }
                }
            }
            form1.txtListCache.value = strData;
        }

        function AddToListe(selectedTrId, tableNo)
        {
            if (form1.txtDateSel.value == "")
                NouvelleListe();
        
            if (form1.txtDateSel.value == "") {
                return;            
            }
                
            var newTR;
            var newTD;

            var selectedTr = document.getElementById(selectedTrId)
            var tableToUse = document.getElementById("tblSelection" + tableNo)


            newTR = tableToUse.insertRow(-1);
            newTR.style.backgroundColor = "White";
            newTR.style.color = "navy";
            newTR.style.cursor = "pointer";
            newTR.onclick = OnSelectTR;

            newTD = newTR.insertCell(-1);
            newTD.style.display = 'none';
            newTD.innerText = selectedTr.cells.item(0).innerText;
            newTD = newTR.insertCell(-1);
            newTD.innerText = selectedTr.cells.item(1).innerText;
            newTD = newTR.insertCell(-1);
            newTD.innerText = selectedTr.cells.item(2).innerText;
            newTD = newTR.insertCell(-1);
            newTD.innerText = selectedTr.cells.item(3).innerText;

            newTD = newTR.insertCell(-1);
            newTD.innerHTML = "<img src='../Images/FlecheHaut.gif' />";
            newTD.title = "Déplacer vers le haut"
            newTD.onclick = PrepareMoveUp;

            newTD = newTR.insertCell(-1);
            newTD.innerHTML = "<img src='../Images/FlecheBas.gif' />";
            newTD.title = "Déplacer vers le bas"
            newTD.onclick = PrepareMoveDown;

            newTD = newTR.insertCell(-1);
            newTD.innerHTML = "<img src='../Images/pictosBoutons/fermer.gif' />";
            newTD.title = "Retirer de la liste"
            newTD.onclick = RemoveFromListe;

            SetSelectionData();
            SelListe();
        }
        
        function OpenWord(Host, userName)
        {
            var today;
            var i;
            var aSection;
            var aSel;
            var aSong;
            var OleTemp;
            
            try
            {
                var oWord = new ActiveXObject("Word.Application");
                oWord.Visible=true;
                oWord.Activate();
                oDoc = oWord.Documents.Add("http://" + Host + "/Utils/Chants.dot");
            }
            catch(e)
            {
                var aWin;
                aWin = window.open("../Utils/Securite.htm", "Securite");
                aWin.focus();
                return;
            }
            

            //Name
            OleTemp = "Nom";
            oDoc.Bookmarks.Item(OleTemp).Select()
            oWord.Selection.Text = userName;

            //date
            OleTemp = "Date"
            oDoc.Bookmarks.Item(OleTemp).Select()
            today = new Date();
            oWord.Selection.Text = form1.txtDateSel.value;

            aSection = form1.txtListCache.value.split("§");
            aSel = aSection[0].split("¦");
            for (i=0;i<aSel.length;i++)
            {
                if (i < 2)
                {
                    aSong = aSel[i].split("¤");
                    OleTemp = "AV_C" + (i + 1);
                    oDoc.Bookmarks.Item(OleTemp).Select();
                    oWord.Selection.Text = aSong[1];
                    OleTemp = "AV_T" + (i + 1);
                    oDoc.Bookmarks.Item(OleTemp).Select();
                    oWord.Selection.Text = aSong[2];
                    OleTemp = "AV_G" + (i + 1);
                    oDoc.Bookmarks.Item(OleTemp).Select();
                    oWord.Selection.Text = aSong[3];
                }
            }    
            
            aSel = aSection[1].split("¦");
            for (i=0;i<aSel.length;i++)
            {
                if (i < 10)
                {
                    aSong = aSel[i].split("¤");
                    OleTemp = "C" + (i + 1);
                    oDoc.Bookmarks.Item(OleTemp).Select();
                    oWord.Selection.Text = aSong[1];
                    OleTemp = "T" + (i + 1);
                    oDoc.Bookmarks.Item(OleTemp).Select();
                    oWord.Selection.Text = aSong[2];
                    OleTemp = "G" + (i + 1);
                    oDoc.Bookmarks.Item(OleTemp).Select();
                    oWord.Selection.Text = aSong[3];
                }
            }       

            aSel = aSection[2].split("¦");
            for (i=0;i<aSel.length;i++)
            {
                if (i < 3)
                {
                    aSong = aSel[i].split("¤");
                    OleTemp = "AP_C" + (i + 1);
                    oDoc.Bookmarks.Item(OleTemp).Select();
                    oWord.Selection.Text = aSong[1];
                    OleTemp = "AP_T" + (i + 1);
                    oDoc.Bookmarks.Item(OleTemp).Select();
                    oWord.Selection.Text = aSong[2];
                    OleTemp = "AP_G" + (i + 1);
                    oDoc.Bookmarks.Item(OleTemp).Select();
                    oWord.Selection.Text = aSong[3];
                }
            }       
        }
        
        function ViderSelection()
        {
            while (tblSelection1.rows.length > 1)
                tblSelection1.deleteRow(1);
            while (tblSelection2.rows.length > 1)
                tblSelection2.deleteRow(1);
            while (tblSelection3.rows.length > 1)
                tblSelection3.deleteRow(1);
            SetSelectionData();
        }
        
        function RemoveFromListe()
        {
            var aTR, aTbl;
            aTR = window.event.srcElement;
            while (aTR.tagName != "TR")
              aTR = aTR.parentElement;
              
            aTbl = aTR;  
            while (aTbl.tagName != "TABLE")
              aTbl = aTbl.parentElement;
              
            aTbl.deleteRow(aTR.rowIndex);
            SetSelectionData();
        }
        
        function PrepareMoveUp()
        {
            sourceElem = window.event.srcElement;
            while (sourceElem.tagName != "TR")
              sourceElem = sourceElem.parentElement;
            setTimeout("MoveUp();", 50);
        }
        function PrepareMoveDown()
        {
            sourceElem = window.event.srcElement;
            while (sourceElem.tagName != "TR")
              sourceElem = sourceElem.parentElement;
            setTimeout("MoveDown();", 50);
        }
        
        function MoveUp()
        {
            var i;
            var aTR1, aTR2, aTbl;
            var index;
            var temp;

            aTR1 = sourceElem;

            aTbl = aTR1;  
            while (aTbl.tagName != "TABLE")
              aTbl = aTbl.parentElement;
          
            index = aTR1.rowIndex;
            if (index > 1)
            {
              aTR2 = aTbl.rows.item(index - 1);  
              for (i=0;i<aTR1.cells.length;i++)
              {
                temp = aTR1.cells.item(i).innerHTML;
                aTR1.cells.item(i).innerHTML = aTR2.cells.item(i).innerHTML;
                aTR2.cells.item(i).innerHTML = temp;
              }
              OnSelectTR(aTR2);
            }
            SetSelectionData();
        }

        function MoveDown()
        {
            var i;
            var aTR1, aTR2, aTbl;
            var index;
            var temp;

            aTR1 = sourceElem;
            aTbl = aTR1;  
            while (aTbl.tagName != "TABLE")
              aTbl = aTbl.parentElement;
            
            index = aTR1.rowIndex;
            if (index < aTbl.rows.length - 1)
            {
              aTR2 = aTbl.rows.item(index + 1);  
              for (i=0;i<aTR1.cells.length;i++)
              {
                temp = aTR1.cells.item(i).innerHTML;
                aTR1.cells.item(i).innerHTML = aTR2.cells.item(i).innerHTML;
                aTR2.cells.item(i).innerHTML = temp;
              }
              OnSelectTR(aTR2);
            }
            SetSelectionData();
       }
        
        
        function ResetTableEvents(table)
        {
            var i;
            //Ne pas réinitialiser les titres
            if (table.rows)
            {
                for (i=1;i<table.rows.length;i++)
                {
                  //effacer la sélection
                  table.rows.item(i).style.backgroundColor = "White";
                  table.rows.item(i).style.color = "navy";
                  
                  //Réaffecter les évenements  
                  table.rows.item(i).onclick = OnSelectTR;
                  table.rows.item(i).cells.item(5).onclick = PrepareMoveUp;
                  table.rows.item(i).cells.item(6).onclick = PrepareMoveDown;
                  table.rows.item(i).cells.item(7).onclick = RemoveFromListe;
                }
            }
        }
        
        function Onload()
        {
            divResultat.style.overflow = "auto";
            divParoles.style.overflow = "auto";
            divSelection.style.overflow = "auto";
        
            OnResize();
            document.body.scroll = "no";
            window.onresize = OnResize;
            
            //Réinitialiser les évenements pour supprimer de la liste
            //Nécessaire à cause des postback
            ResetTableEvents(tblSelection1);
            ResetTableEvents(tblSelection2);
            ResetTableEvents(tblSelection3);
            
            if (form1.txtSelectedTab.value == "tdSel")
                SelListe();
        }
        
        function SelListe()
        {
            tdSel.className = 'TabSelected';
            tdParoles.className = 'TabUnSelected';
            tdAccords.className = 'TabUnSelected';

            trSel.style.display='';
            trParoles.style.display = 'none';
            trAccords.style.display = 'none';
            form1.txtSelectedTab.value = "tdSel";
 
            form1.txtDateSel.style.color = 'Navy';
       }
        
        function SelParoles()
        {
            tdSel.className = 'TabUnSelected';
            tdParoles.className = 'TabSelected';
            tdAccords.className = 'TabUnSelected';

            trSel.style.display='none';
            trParoles.style.display='';
            trAccords.style.display = 'none';
            form1.txtSelectedTab.value = "tdParoles";
            
            form1.txtDateSel.style.color = '#F0F0FF';
        }

        function SelAccords() {
            tdSel.className = 'TabUnSelected';
            tdParoles.className = 'TabUnSelected';
            tdAccords.className = 'TabSelected';

            trSel.style.display = 'none';
            trParoles.style.display = 'none';
            trAccords.style.display = '';
            form1.txtSelectedTab.value = "tdAccords";

            form1.txtDateSel.style.color = '#F0F0FF';
        }
