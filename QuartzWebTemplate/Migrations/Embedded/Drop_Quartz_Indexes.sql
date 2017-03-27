drop index idx_qrtz_t_next_fire_time ON qrtz_triggers;
drop index idx_qrtz_t_state ON qrtz_triggers;
drop index idx_qrtz_t_nf_st ON qrtz_triggers;
drop index idx_qrtz_ft_trig_name ON qrtz_fired_triggers;
drop index idx_qrtz_ft_trig_group ON qrtz_fired_triggers;
drop index idx_qrtz_ft_trig_n_g ON qrtz_fired_triggers
drop index idx_qrtz_ft_trig_inst_name ON qrtz_fired_triggers;
drop index idx_qrtz_ft_job_name ON qrtz_fired_triggers;
drop index idx_qrtz_ft_job_group ON qrtz_fired_triggers;
drop index idx_qrtz_t_next_fire_time_misfire ON qrtz_triggers;
drop index idx_qrtz_t_nf_st_misfire ON qrtz_triggers;
drop index idx_qrtz_t_nf_st_misfire_grp ON qrtz_triggers;

GO