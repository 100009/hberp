  select 'insert into admin_dept_list(dept_id,dept_desc,status,update_user,update_time)values('''+ dept_id +''','''+ dept_desc+''',1,'''+update_user+''',getdate())' from admin_dept_list;