create procedure spCliente
  @ClienteId int=null, 
  @Nombre varchar(100)=null,
  @Ciudad varchar(50)=null,
  @modo varchar(25) 
as 
  begin 
    set nocount on; 
    if (@modo='guardar') 
    begin 
	if not exists (select * from tblCliente where ClienteId=@ClienteId)
      insert into tblCliente (Nombre, Ciudad) 
                  values (@Nombre, @Ciudad) 
    end 
    if (@modo='actualizar') 
    begin 
      update tblCliente 
      set    Nombre=@Nombre, 
             Ciudad=@Ciudad
      where  ClienteId=@ClienteId 
    end 
    if (@modo='eliminar') 
    begin 
      delete 
      from   tblCliente 
      where  ClienteId=@ClienteId 
    end 
    if (@modo='mostrar') 
    begin 
      select ClienteId, 
             Nombre, 
             Ciudad
      from   tblCliente 
    end 
    if (@modo='mostrarid') 
    begin 
      select ClienteId, 
             Nombre, 
             Ciudad
      from   tblCliente 
      where  ClienteId=@ClienteId
    end
end